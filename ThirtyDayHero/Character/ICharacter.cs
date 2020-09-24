namespace ThirtyDayHero
{
    public interface ICharacter : ICombatEntity
    {
        ICharacterClass Class { get; }
        IStatMap Stats { get; }
        
        float GetReducedDamage(float damageAmount, DamageType damageType);
    }
}