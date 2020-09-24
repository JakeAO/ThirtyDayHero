namespace ThirtyDayHero
{
    public interface IPlayerClass : ICharacterClass
    {
        WeaponType WeaponProficiency { get; }
        ArmorType ArmorProficiency { get; }
        IEquipMapBuilder StartingEquipment { get; }
    }
}