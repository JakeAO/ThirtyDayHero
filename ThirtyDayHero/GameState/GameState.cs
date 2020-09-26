using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class GameState : IGameState
    {
        private static uint _nextId = 150000;
        public static uint NextId => ++_nextId;
        
        public uint Id { get; }
        public CombatState State { get; }
        public IInitiativeActor ActiveActor { get; }
        public IReadOnlyList<IInitiativePair> InitiativeOrder { get; }

        public GameState(
            CombatState state,
            IInitiativeActor activeActor,
            IReadOnlyList<IInitiativePair> initiative)
        {
            Id = NextId;
            State = state;
            ActiveActor = activeActor;
            InitiativeOrder = new List<IInitiativePair>(initiative);
        }
    }
}