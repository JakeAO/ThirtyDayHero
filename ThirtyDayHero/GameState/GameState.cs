using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class GameState : IGameState
    {
        private static uint _nextId = 150000;
        public static uint NextId => ++_nextId;
        
        public uint Id { get; }
        public bool ActionPending { get; }
        public CombatState State { get; }
        public IInitiativeActor ActiveActor { get; }
        public IReadOnlyList<IInitiativePair> InitiativeOrder { get; }

        public GameState(
            bool actionPending,
            CombatState state,
            IInitiativeActor activeActor,
            IReadOnlyList<IInitiativePair> initiative)
        {
            Id = NextId;
            ActionPending = actionPending;
            State = state;
            ActiveActor = activeActor;
            InitiativeOrder = new List<IInitiativePair>(initiative);
        }
    }
}