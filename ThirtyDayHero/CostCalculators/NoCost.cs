namespace ThirtyDayHero
{
    public class NoCost : ICostCalc
    {
        public static readonly NoCost Instance = new NoCost();
        
        public bool CanAfford(ICombatEntity entity)
        {
            return true;
        }

        public void Pay(ICombatEntity entity)
        {
            // Intentionally left blank
        }
    }
}