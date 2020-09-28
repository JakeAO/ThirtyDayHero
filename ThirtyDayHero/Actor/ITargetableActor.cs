namespace ThirtyDayHero
{
    public interface ITargetableActor : IInitiativeActor
    {
        bool CanTarget { get; }
    }
}