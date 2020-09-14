using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirtyDayHero
{
    public class CombatManager
    {
        private static readonly Random RANDOM = new Random();

        private readonly List<IParty> _parties = new List<IParty>(2);
        private readonly List<ICharacter> _characters = new List<ICharacter>(10);
        private readonly List<(ICharacter, float)> _characterInitiativeList = new List<(ICharacter, float)>(10);

        private readonly IReadOnlyDictionary<uint, ICharacterController> _controllerByPartyId;

        private readonly Action<uint> _onCombatComplete = null;

        private ICharacter _activeCharacter = null;
        private IReadOnlyDictionary<uint, IAction> _currentActionMap = null;

        private static void IncrementInitiative((ICharacter, float) val) => val.Item2 += val.Item1.Stats.GetStat(StatType.DEX);

        private static uint GetWinningPartyId(IReadOnlyCollection<ICharacter> allCharacters) =>
            allCharacters
                .Where(x => x.Stats.GetStat(StatType.HP) > 0u)
                .Select(x => x.Party)
                .Distinct()
                .SingleOrDefault();

        private static ICharacter GetNextCharacter(IReadOnlyCollection<(ICharacter, float)> allCharacters) =>
            allCharacters
                .Where(kvp => kvp.Item1.Stats.GetStat(StatType.HP) > 0u && kvp.Item2 >= 100f)
                .OrderByDescending(kvp => kvp.Item2)
                .First()
                .Item1;

        public CombatManager(IReadOnlyCollection<IParty> parties)
        {
            _parties.AddRange(parties);
            _characters.AddRange(parties.SelectMany(x => x.Characters));
            _characterInitiativeList.AddRange(_characters.Select(x => (x, (float) (RANDOM.NextDouble() * x.Stats.GetStat(StatType.DEX)))));

            _characterInitiativeList.ForEach(IncrementInitiative);

            _controllerByPartyId = _parties.ToDictionary(x => x.Id, x => x.Controller);
        }

        public void TriggerNextState()
        {
            // Check End State Condition
            uint winningPartyId = GetWinningPartyId(_characters);
            if (winningPartyId != 0u)
            {
                _onCombatComplete(winningPartyId);
                return;
            }

            // Get Next Active Character
            if (_activeCharacter == null)
            {
                while ((_activeCharacter = GetNextCharacter(_characterInitiativeList)) == null)
                {
                    _characterInitiativeList.ForEach(IncrementInitiative);
                }

                _currentActionMap = _activeCharacter
                    .GetAllActions(_characters)
                    .ToDictionary(x => x.Id);
            }

            // Notify Active Character Controller of Actions Available
            ICharacterController controller = _controllerByPartyId[_activeCharacter.Party];
            controller.SelectAction(_activeCharacter, _currentActionMap, ActionSelected);
        }

        public void ActionSelected(uint actionId)
        {
            if (_currentActionMap.TryGetValue(actionId, out IAction action))
            {
                (ICharacter, float) sourceInitiative = _characterInitiativeList.Find(x => x.Item1.Id == action.Source.Id);

                sourceInitiative.Item2 -= action.Ability.Speed;
                action.Ability.Cost.Pay(action.Source);
                action.Ability.Effect.Apply(action.Source, action.Targets);

                _activeCharacter = null;
                _currentActionMap = null;

                TriggerNextState();
            }
        }
    }
}