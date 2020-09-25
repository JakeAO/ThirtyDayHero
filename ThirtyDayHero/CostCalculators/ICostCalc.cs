namespace ThirtyDayHero
{
    public interface ICostCalc
    {
        bool CanAfford(IInitiativeActor entity);
        void Pay(IInitiativeActor entity);
    }
}