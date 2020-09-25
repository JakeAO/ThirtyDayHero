using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class GameState : IGameState
    {
        public IReadOnlyList<IInitiativePair> InitiativeOrder { get; }
        public IInitiativeActor ActiveActor { get; }

        public GameState(
            IReadOnlyList<IInitiativePair> initiative,
            IInitiativeActor activeActor)
        {
            InitiativeOrder = new List<IInitiativePair>(initiative);
            ActiveActor = activeActor;
        }
    }
}