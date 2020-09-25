namespace ThirtyDayHero
{
    public class NoCost : ICostCalc
    {
        public static readonly NoCost Instance = new NoCost();
        
        public bool CanAfford(IInitiativeActor entity)
        {
            return true;
        }

        public void Pay(IInitiativeActor entity)
        {
            // Intentionally left blank
        }
    }
}