using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IGameState
    {
        IReadOnlyList<IInitiativePair> InitiativeOrder { get; }
        IInitiativeActor ActiveActor { get; }
    }
}