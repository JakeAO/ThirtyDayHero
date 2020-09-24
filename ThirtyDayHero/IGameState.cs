using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IGameState
    {
        IReadOnlyList<ICombatEntity> Entities { get; }
        IReadOnlyList<CombatManager.InitiativePair> Initiative { get; }
        
    }
}