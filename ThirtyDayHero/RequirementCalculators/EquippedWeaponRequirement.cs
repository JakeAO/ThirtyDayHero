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
            if (character is IPlayerCharacter playerCharacter)
            {
                WeaponType equippedType = playerCharacter.Equipment.Weapon?.WeaponType ?? WeaponType.Invalid;
                return (equippedType & _weaponType) == equippedType;
            }

            return false;
        }
    }
}