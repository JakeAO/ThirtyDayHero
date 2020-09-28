namespace ThirtyDayHero
{
    public interface ICharacterActor : ITargetableActor
    {
        ICharacterClass Class { get; }
        IStatMap Stats { get; }
        
        float GetReducedDamage(float damageAmount, DamageType damageType);
    }
}