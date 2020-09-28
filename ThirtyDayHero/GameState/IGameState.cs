using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IGameState
    {
        uint Id { get; }
        bool ActionPending { get; }
        CombatState State { get; }
        IInitiativeActor ActiveActor { get; }
        IReadOnlyList<IInitiativePair> InitiativeOrder { get; }
    }
}