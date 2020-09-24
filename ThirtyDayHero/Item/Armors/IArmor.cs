namespace ThirtyDayHero
{
    public interface IArmor : IItem
    {
        ArmorType ArmorType { get; }

        float GetReducedDamage(float damageAmount, DamageType damageType);
    }
}