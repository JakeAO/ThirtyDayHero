using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ICharacter
    {
        uint Id { get; }
        uint Party { get; }
        string Name { get; }
        ICharacterClass Class { get; }
        IStatMap Stats { get; }
        IEquipMap Equipment { get; }

        IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ICharacter> allCharacters);
    }
}