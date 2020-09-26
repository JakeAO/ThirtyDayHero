using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirtyDayHero
{
    public class CombatManager
    {
        private static readonly Random RANDOM = new Random();

        private readonly List<IParty> _parties = new List<IParty>(2);
        private readonly List<IInitiativeActor> _entities = new List<IInitiativeActor>(10);
        private readonly List<ICharacterActor> _characters = new List<ICharacterActor>(10);
        private readonly List<InitiativePair> _characterInitiativeList = new List<InitiativePair>(10);

        private readonly IReadOnlyDictionary<uint, ICharacterController> _controllerByPartyId;

        public event Action<IAction> ActionTaken; 
        public event Action<IGameState> GameStateUpdate;
        public event Action<uint> CombatComplete;

        private GameState _currentGameState = null;
        
        private CombatState _state = CombatState.Invalid;
        private IInitiativeActor _activeEntity = null;
        private IReadOnlyDictionary<uint, IAction> _currentActionMap = null;

        public IGameState CurrentGameState => _currentGameState;

        private void UpdateCurrentGameState()
        {
            _currentGameState = new GameState(_state, _activeEntity, _characterInitiativeList);
        }

        private bool GetWinningPartyId(out uint winningParty)
        {
            winningParty = 0u;

            foreach (ICharacterActor character in _characters)
            {
                if (character == null)
                    continue;
                if (character.Stats.GetStat(StatType.HP) <= 0u)
                    continue;

                if (winningParty == 0)
                {
                    winningParty = character.Party;
                }
                else if (winningParty != character.Party)
                {
                    return false;
                }
            }

            return winningParty != 0u;
        }

        private void IncrementInitiative()
        {
            foreach (InitiativePair tuple in _characterInitiativeList)
            {
                tuple.Initiative += tuple.Entity.Initiative;
            }

            _characterInitiativeList.Sort((lhs, rhs) => rhs.Initiative.CompareTo(lhs.Initiative));
        }

        private IInitiativeActor GetNextEntity()
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
            _characters.AddRange(_entities.Select(x => x as ICharacterActor).Where(x => x != null));
            _characterInitiativeList.AddRange(_entities.Select(x =>
                new InitiativePair(x, (float) (RANDOM.NextDouble() * x.Initiative))));

            _controllerByPartyId = _parties.ToDictionary(x => x.Id, x => x.Controller);

            _state = CombatState.Invalid;
            UpdateCurrentGameState();
        }

        public void Start()
        {
            Continue();
        }

        public void Continue()
        {
            // Check End State Condition
            if (GetWinningPartyId(out uint winningParty))
            {
                _state = CombatState.Completed;
                UpdateCurrentGameState();
                GameStateUpdate?.Invoke(_currentGameState);
                CombatComplete?.Invoke(winningParty);
                return;
            }

            _state = CombatState.Active;

            // Get Next Active Character
            if (_activeEntity == null)
            {
                IInitiativeActor nextActor = GetNextEntity();
                if (!nextActor.Alive)
                {
                    InitiativePair nextActorInit = _characterInitiativeList.Find(x => x.Entity.Id == nextActor.Id);
                    nextActorInit.Initiative -= 100f;
                    Continue();
                    return;
                }

                _activeEntity = nextActor;
                _currentActionMap = _activeEntity
                    .GetAllActions(_characters)
                    .ToDictionary(x => x.Id);
            }

            // Update and Send GameState
            UpdateCurrentGameState();
            GameStateUpdate?.Invoke(_currentGameState);

            // Notify Active Character Controller of Actions Available
            ICharacterController controller = _controllerByPartyId[_activeEntity.Party];
            controller.SelectAction(_activeEntity, _currentActionMap, OnActionSelected);
        }

        private void OnActionSelected(uint actionId)
        {
            if (_currentActionMap.TryGetValue(actionId, out IAction action))
            {
                if (action.Available)
                {
                    InitiativePair sourceInitiative = _characterInitiativeList.Find(x => x.Entity.Id == action.Source.Id);

                    sourceInitiative.Initiative -= action.Ability.Speed;
                    action.Ability.Cost.Pay(action.Source);
                    action.Ability.Effect.Apply(action.Source, action.Targets);

                    _activeEntity = null;
                    _currentActionMap = null;

                    ActionTaken?.Invoke(action);
                }

                // Check End State Condition
                if (GetWinningPartyId(out uint winningParty))
                {
                    _state = CombatState.Completed;
                    CombatComplete?.Invoke(winningParty);
                }
            }
        }
    }
}