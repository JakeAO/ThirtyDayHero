namespace ThirtyDayHero
{
    public interface IArmor : IItem
    {
        ArmorType ArmorType { get; }

        uint GetReducedDamage(uint damageAmount, DamageType damageType);
    }
}