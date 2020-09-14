namespace ThirtyDayHero
{
    public class EquippedWeaponRequirement : IRequirementCalc
    {
        private WeaponType _weaponType = WeaponType.Invalid;

        public EquippedWeaponRequirement(WeaponType weaponType)
        {
            _weaponType = weaponType;
        }

        public bool MeetsRequirement(ICharacter character)
        {
            WeaponType equippedType = character.Equipment.Weapon?.WeaponType ?? WeaponType.Invalid;
            return (equippedType & _weaponType) == equippedType;
        }
    }
}