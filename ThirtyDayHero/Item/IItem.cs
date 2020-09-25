using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IItem
    {
        uint Id { get; }
        string Name { get; }
        string Desc { get; }
        ItemType ItemType { get; }

        IReadOnlyCollection<IAction> GetAllActions(ICharacterActor sourceCharacter, IReadOnlyCollection<ICharacterActor> allCharacters, bool isEquipped);

        // TODO Stat Bonus
    }
}