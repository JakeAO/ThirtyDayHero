namespace ThirtyDayHero
{
    public interface IInitiativePair
    {
        IInitiativeActor Entity { get; }
        float Initiative { get; }
    }
}