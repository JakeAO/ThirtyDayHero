using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ICharacterClass
    {
        uint Id { get; }
        string Name { get; }
        string Desc { get; }
        IStatMapBuilder StartingStats { get; }
        IStatMapIncrementor LevelUpStats { get; }
        WeaponType WeaponProficiency { get; }
        ArmorType ArmorProficiency { get; }
        IReadOnlyDictionary<uint, IReadOnlyCollection<IAbility>> AbilitiesPerLevel { get; }

        IReadOnlyCollection<IAbility> GetAllAbilities(uint level);
    }
}