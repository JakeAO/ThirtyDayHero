using System;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Town
{
    public class TownInnState : BlazorState
    {
        public override Type RenderType => typeof(TownInnStatePage);

        public uint Cost { get; }
        public PartyDataWrapper Party { get; private set; }
        public bool HasRested { get; private set; }

        public TownInnState(uint innCost)
        {
            Cost = innCost;
        }

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Party = context.Get<PartyDataWrapper>();

            HasRested = false;
        }

        public bool CanStayAtInn()
        {
            return Party.Gold >= Cost;
        }

        public void StayAtInn()
        {
            HasRested = true;
            Party.Gold -= Cost;
            foreach (PlayerCharacter playerCharacter in Party.Characters)
            {
                playerCharacter.Stats.ModifyStat(StatType.HP, (int) playerCharacter.Stats[StatType.HP_Max]);
                playerCharacter.Stats.ModifyStat(StatType.STA, (int) playerCharacter.Stats[StatType.STA_Max]);
            }
        }

        public void LeaveInn()
        {
            _context.Get<IStateMachine>().ChangeState<TownHubState>();
        }
    }
}