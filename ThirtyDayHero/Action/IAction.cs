using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IAction
    {
        uint Id { get; }
        bool Available { get; }
        IAbility Ability { get; }
        ICharacter Source { get; }
        IReadOnlyCollection<ICharacter> Targets { get; }
    }
}