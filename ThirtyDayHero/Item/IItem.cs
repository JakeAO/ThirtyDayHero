using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IItem
    {
        uint Id { get; }
        string Name { get; }
        string Desc { get; }
        ItemType ItemType { get; }

        IReadOnlyCollection<IAction> GetAllActions(ICharacterActor sourceCharacter, IReadOnlyCollection<ITargetableActor> possibleTargets, bool isEquipped);

        // TODO Stat Bonus
    }
}