using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirtyDayHero
{
    public class CombatManager
    {
        public class InitiativePair
        {
            public readonly ICombatEntity Entity;
            public float Initiative;

            public InitiativePair(ICombatEntity entity, float init)
            {
                Entity = entity;
                Initiative = init;
            }
        }

        private static readonly Random RANDOM = new Random();

        private readonly List<IParty> _parties = new List<IParty>(2);
        private readonly List<ICombatEntity> _entities = new List<ICombatEntity>(10);
        private readonly List<ICharacter> _characters = new List<ICharacter>(10);
        private readonly List<InitiativePair> _characterInitiativeList = new List<InitiativePair>(10);

        private readonly IReadOnlyDictionary<uint, ICharacterController> _controllerByPartyId;

        private readonly Action<uint> _onCombatComplete = null;

        private ICombatEntity _activeEntity = null;
        private IReadOnlyDictionary<uint, IAction> _currentActionMap = null;

        private static uint GetWinningPartyId(IReadOnlyCollection<ICombatEntity> allCharacters) =>
            allCharacters
                .Select(x => x as ICharacter)
                .Where(x => x?.Stats.GetStat(StatType.HP) > 0u)
                .Select(x => x.Party)
                .Distinct()
                .SingleOrDefault();

        private void IncrementInitiative()
        {
            foreach (InitiativePair tuple in _characterInitiativeList)
            {
                tuple.Initiative += tuple.Entity.Initiative;
            }

            _characterInitiativeList.Sort((lhs, rhs) => rhs.Initiative.CompareTo(lhs.Initiative));
        }

        private ICombatEntity GetNextEntity()
        {
            while (_characterInitiativeList[0].Initiative < 100f)
            {
                IncrementInitiative();
            }

            return _characterInitiativeList[0].Entity;
        }

        public CombatManager(IReadOnlyCollection<IParty> parties)
        {
            _parties.AddRange(parties);
            _entities.AddRange(parties.SelectMany(x => x.Characters));
            _characters.AddRange(_entities.Select(x => x as ICharacter).Where(x => x != null));
            _characterInitiativeList.AddRange(_entities.Select(x => new InitiativePair(x, (float) (RANDOM.NextDouble() * x.Initiative))));

            _controllerByPartyId = _parties.ToDictionary(x => x.Id, x => x.Controller);
        }

        public void TriggerNextState()
        {
            // Check End State Condition
            uint winningPartyId = GetWinningPartyId(_entities);
            if (winningPartyId != 0u)
            {
                _onCombatComplete(winningPartyId);
                return;
            }

            // Get Next Active Character
            if (_activeEntity == null)
            {
                _activeEntity = GetNextEntity();

                _currentActionMap = _activeEntity
                    .GetAllActions(_characters)
                    .ToDictionary(x => x.Id);
            }

            // Notify Active Character Controller of Actions Available
            ICharacterController controller = _controllerByPartyId[_activeEntity.Party];
            controller.SelectAction(_activeEntity, _currentActionMap, ActionSelected);
        }

        public void ActionSelected(uint actionId)
        {
            if (_currentActionMap.TryGetValue(actionId, out IAction action))
            {
                InitiativePair sourceInitiative = _characterInitiativeList.Find(x => x.Entity.Id == action.Source.Id);

                sourceInitiative.Initiative -= action.Ability.Speed;
                action.Ability.Cost.Pay(action.Source);
                action.Ability.Effect.Apply(action.Source, action.Targets);

                _activeEntity = null;
                _currentActionMap = null;

                TriggerNextState();
            }
        }
    }
}