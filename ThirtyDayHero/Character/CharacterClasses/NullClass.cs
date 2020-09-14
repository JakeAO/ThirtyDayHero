using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class NullClass : ICharacterClass
    {
        public static readonly NullClass Instance = new NullClass();
        
        public uint Id => 0u;
        public string Name => string.Empty;
        public string Desc => string.Empty;
        public IStatMapBuilder StartingStats => NullStatMapBuilder.Instance;
        public IStatMapIncrementor LevelUpStats => NullStatMapIncrementor.Instance;
        public WeaponType WeaponProficiency => WeaponType.Invalid;
        public ArmorType ArmorProficiency => ArmorType.Invalid;
        public IReadOnlyDictionary<uint, IReadOnlyCollection<IAbility>> AbilitiesPerLevel { get; } = new Dictionary<uint, IReadOnlyCollection<IAbility>>();

        public IReadOnlyCollection<IAbility> GetAllAbilities(uint level)
        {
            return new IAbility[] { };
        }
    }
}