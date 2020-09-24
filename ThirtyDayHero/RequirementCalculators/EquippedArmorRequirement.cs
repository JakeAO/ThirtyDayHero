namespace ThirtyDayHero
{
    public class EquippedArmorRequirement : IRequirementCalc
    {
        private readonly ArmorType _armorType = ArmorType.Invalid;

        public EquippedArmorRequirement(ArmorType armorType)
        {
            _armorType = armorType;
        }

        public bool MeetsRequirement(ICharacter character)
        {
            if (character is IPlayerCharacter playerCharacter)
            {
                ArmorType equippedType = playerCharacter.Equipment.Armor?.ArmorType ?? ArmorType.Invalid;
                return (equippedType & _armorType) == equippedType;
            }

            return false;
        }
    }
}