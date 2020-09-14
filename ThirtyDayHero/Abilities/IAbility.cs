namespace ThirtyDayHero
{
    public interface IAbility
    {
        uint Id { get; }
        string Name { get; }
        string Desc { get; }
        uint Speed { get; }
        IRequirementCalc Requirements { get; }
        ICostCalc Cost { get; }
        ITargetCalc Target { get; }
        IEffectCalc Effect { get; }
    }
}