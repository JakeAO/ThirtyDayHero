using System;
using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Util.CombatEngine.Abilities;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Combat
{
    public class CombatEndState : BlazorState
    {
        public override Type RenderType => typeof(CombatEndStatePage);

        public PartyDataWrapper Party { get; private set; }
        public CombatResults Results { get; private set; }

        public IReadOnlyDictionary<uint, IStatMap> OldStatMapsByCharacter => _oldStatMaps;
        public IReadOnlyDictionary<uint, uint[]> StatChangesByCharacter => _statChanges;
        public IReadOnlyDictionary<uint, IAbility[]> GainedAbilitiesByCharacter => _gainedAbilities;

        private readonly Dictionary<uint, IStatMap> _oldStatMaps = new Dictionary<uint, IStatMap>();
        private readonly Dictionary<uint, uint[]> _statChanges = new Dictionary<uint, uint[]>();
        private readonly Dictionary<uint, IAbility[]> _gainedAbilities = new Dictionary<uint, IAbility[]>();

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Results = context.Get<CombatResults>();
            Party = context.Get<PartyDataWrapper>();
            FirebaseWrapper fbWrapper = context.Get<FirebaseWrapper>();

            context.Clear<CombatResults>();
            
            if (Results.Success)
            {
                Random random = new Random();

                Party.Gold += Results.GoldReward;
                Party.Inventory.AddRange(Results.ItemReward);
                Party.CalamityDefeated = Party.Day >= 30;
                
                fbWrapper.WriteData(Party.GetDataPath(fbWrapper.UserId), Party);

                // Update Stats
                StatType[] statTypes = Enum.GetValues(typeof(StatType)).Cast<StatType>().ToArray();
                uint expPerCharacter = (uint) Math.Ceiling(Results.ExpReward / (float) Party.Characters.Count);
                foreach (PlayerCharacter playerCharacter in Party.Characters)
                {
                    IStatMap oldMap = playerCharacter.Stats.Copy();
                    IStatMap newMap = playerCharacter.Stats;

                    // Add EXP
                    newMap.ModifyStat(StatType.EXP, (int) expPerCharacter);

                    // Level Up
                    uint lvlsUp = newMap[StatType.LVL] - oldMap[StatType.LVL];
                    for (int i = 0; i < lvlsUp; i++)
                    {
                        newMap = playerCharacter.Class.LevelUpStats.Increment(newMap, random);
                    }

                    playerCharacter.Stats = newMap;

                    // Calc Changes
                    uint[] changes = new uint[statTypes.Length];
                    for (int i = 0; i < statTypes.Length; i++)
                    {
                        changes[i] = newMap[(StatType) i] - oldMap[(StatType) i];
                    }
                    changes[(int) StatType.EXP] = newMap[StatType.EXP] + 100 * lvlsUp - oldMap[StatType.EXP];
                    
                    _oldStatMaps[playerCharacter.Id] = oldMap;
                    _statChanges[playerCharacter.Id] = changes;
                    _gainedAbilities[playerCharacter.Id] =
                        playerCharacter.Class.GetAllAbilities(newMap[StatType.LVL])
                            .Except(playerCharacter.Class.GetAllAbilities(oldMap[StatType.LVL]))
                            .ToArray();
                }
            }
            else
            {
                PlayerDataWrapper playerData = _context.Get<PlayerDataWrapper>();
                playerData.SetActiveParty(0u);

                fbWrapper.WriteData(Party.GetDataPath(fbWrapper.UserId), Party);
                fbWrapper.WriteData(playerData.GetDataPath(fbWrapper.UserId), playerData);

                _context.Clear<PartyDataWrapper>();
            }
        }

        public void Continue()
        {
            IStateMachine stateMachine = _context.Get<IStateMachine>();
            if (Party.CalamityDefeated)
            {
                stateMachine.ChangeState<CalamityDefeatedState>();
            }
            else if (Results.Success)
            {
                stateMachine.ChangeState<GameplayState>();
            }
            else
            {
                stateMachine.ChangeState<CreatePartyState>();
            }
        }
    }
}