namespace ThirtyDayHero
{
    public interface IWeapon : IItem
    {
        WeaponType WeaponType { get; }
    }
}