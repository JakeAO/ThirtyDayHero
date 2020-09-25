using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IAction
    {
        uint Id { get; }
        bool Available { get; }
        IAbility Ability { get; }
        IInitiativeActor Source { get; }
        IReadOnlyCollection<ICharacterActor> Targets { get; }
    }
}