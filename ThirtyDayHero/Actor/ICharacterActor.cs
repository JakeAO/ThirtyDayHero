namespace ThirtyDayHero
{
    public interface ICharacterActor : IInitiativeActor
    {
        ICharacterClass Class { get; }
        IStatMap Stats { get; }
        
        float GetReducedDamage(float damageAmount, DamageType damageType);
    }
}