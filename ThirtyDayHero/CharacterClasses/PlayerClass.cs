using System.Collections.Generic;

namespace ThirtyDayHero.CharacterClasses
{
    public class PlayerClass : CharacterClass, IPlayerClass
    {
        public WeaponType WeaponProficiency { get; }
        public ArmorType ArmorProficiency { get; }
        public IEquipMapBuilder StartingEquipment { get; }

        public PlayerClass(
            uint id,
            string name,
            string desc,
            IReadOnlyDictionary<DamageType, float> intrinsicDamageModification,
            IStatMapBuilder startingStats,
            IStatMapIncrementor levelUpStats,
            IReadOnlyDictionary<uint, IReadOnlyCollection<IAbility>> abilitiesPerLevel,
            WeaponType weaponProficiency,
            ArmorType armorProficiency,
            IEquipMapBuilder startingEquipment)
            : base(
                id,
                name,
                desc,
                intrinsicDamageModification,
                startingStats,
                levelUpStats,
                abilitiesPerLevel)
        {
            WeaponProficiency = weaponProficiency;
            ArmorProficiency = armorProficiency;
            StartingEquipment = startingEquipment;
        }
    }
}