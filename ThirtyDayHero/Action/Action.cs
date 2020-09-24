using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class Action : IAction
    {
        public uint Id { get; }
        public bool Available { get; }
        public IAbility Ability { get; }
        public ICombatEntity Source { get; }
        public IReadOnlyCollection<ICharacter> Targets { get; }

        public Action(
            uint id,
            bool available,
            IAbility ability,
            ICombatEntity source,
            IReadOnlyCollection<ICharacter> targets)
        {
            Id = id;
            Available = available;
            Ability = ability;
            Source = source;
            Targets = targets;
        }
    }
}