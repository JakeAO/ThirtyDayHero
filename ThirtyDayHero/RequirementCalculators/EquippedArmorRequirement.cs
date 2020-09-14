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
            ArmorType equippedType = character.Equipment.Armor?.ArmorType ?? ArmorType.Invalid;
            return (equippedType & _armorType) == equippedType;
        }
    }
}