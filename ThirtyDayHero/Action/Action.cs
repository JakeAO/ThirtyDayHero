using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class Action : IAction
    {
        public uint Id { get; }
        public bool Available { get; }
        public IAbility Ability { get; }
        public IInitiativeActor Source { get; }
        public IReadOnlyCollection<ICharacterActor> Targets { get; }

        public Action(
            uint id,
            bool available,
            IAbility ability,
            IInitiativeActor source,
            IReadOnlyCollection<ICharacterActor> targets)
        {
            Id = id;
            Available = available;
            Ability = ability;
            Source = source;
            Targets = targets;
        }
    }
}