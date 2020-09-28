using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirtyDayHero
{
    public class CombatManager
    {
        private static readonly Random RANDOM = new Random();

        private readonly List<IParty> _parties = new List<IParty>(2);
        private readonly List<ITargetableActor> _allTargets = new List<ITargetableActor>(10);
        private readonly List<InitiativePair> _actorInitiativeList = new List<InitiativePair>(10);
        private readonly Dictionary<uint, ICharacterController> _controllerByPartyId = new Dictionary<uint, ICharacterController>(2);

        public event Action<IAction> ActionTaken;
        public event Action<IGameState> GameStateUpdate;
        public event Action<uint> CombatComplete;

        private GameState _currentGameState = null;

        private CombatState _state = CombatState.Invalid;
        private IInitiativeActor _activeEntity = null;
        private IReadOnlyDictionary<uint, IAction> _currentActionMap = null;

        public IGameState CurrentGameState => _currentGameState;

        private void UpdateCurrentGameState(bool actionPending)
        {
            _currentGameState = new GameState(actionPending, _state, _activeEntity, _actorInitiativeList);
        }

        private bool GetWinningPartyId(out uint winningParty)
        {
            winningParty = 0u;

            foreach (InitiativePair initPair in _actorInitiativeList)
            {
                IInitiativeActor actor = initPair.Entity;
                if (actor == null)
                    continue;
                if (!actor.Alive)
                    continue;

                if (winningParty == 0)
                {
                    winningParty = actor.Party;
                }
                else if (winningParty != actor.Party)
                {
                    return false;
                }
            }

            return winningParty != 0u;
        }

        private void IncrementInitiative()
        {
            foreach (InitiativePair tuple in _actorInitiativeList)
            {
                tuple.Initiative += tuple.Entity.Initiative;
            }

            _actorInitiativeList.Sort((lhs, rhs) => rhs.Initiative.CompareTo(lhs.Initiative));
        }

        private InitiativePair GetNextEntity()
        {
            while (_actorInitiativeList[0].Initiative < 100f)
            {
                IncrementInitiative();
            }

            return _actorInitiativeList[0];
        }

        public CombatManager(IReadOnlyCollection<IParty> parties)
        {
            _parties.AddRange(parties);
            foreach (IParty party in parties)
            {
                _controllerByPartyId[party.Id] = party.Controller ?? new RandomCharacterController();
                foreach (IInitiativeActor actor in party.Actors)
                {
                    if (actor is ITargetableActor targetableActor)
                        _allTargets.Add(targetableActor);

                    _actorInitiativeList.Add(new InitiativePair(actor, 0));
                }
            }

            _state = CombatState.Invalid;
            UpdateCurrentGameState(false);
        }

        public void Start()
        {
            _state = CombatState.Active;
            UpdateCurrentGameState(false);
            GameStateUpdate?.Invoke(_currentGameState);
        }

        public void Continue()
        {
            // Check End State Condition
            if (GetWinningPartyId(out uint _))
            {
                return;
            }

            _state = CombatState.Active;

            // Get Next Active Character
            if (_activeEntity == null)
            {
                InitiativePair initPair = GetNextEntity();
                IInitiativeActor initActor = initPair.Entity;
                if (!initActor.Alive)
                {
                    initPair.Initiative -= 100f;
                    Continue();
                    return;
                }

                _activeEntity = initActor;
                _currentActionMap = _activeEntity
                    .GetAllActions(_allTargets)
                    .ToDictionary(x => x.Id);
            }

            // Update and Send GameState
            UpdateCurrentGameState(true);
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
                    InitiativePair sourceInitiative = _actorInitiativeList.Find(x => x.Entity.Id == action.Source.Id);
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
                    UpdateCurrentGameState(false);
                    GameStateUpdate?.Invoke(_currentGameState);
                    CombatComplete?.Invoke(winningParty);
                }
                else
                {
                    UpdateCurrentGameState(false);
                    GameStateUpdate?.Invoke(_currentGameState);
                }
            }
        }
    }
}