using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IAction
    {
        uint Id { get; }
        bool Available { get; }
        IAbility Ability { get; }
        ICombatEntity Source { get; }
        IReadOnlyCollection<ICharacter> Targets { get; }
    }
}