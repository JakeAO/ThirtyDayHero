namespace ThirtyDayHero
{
    public class NoCost : ICostCalc
    {
        public static readonly NoCost Instance = new NoCost();
        
        public bool CanAfford(ICharacter character)
        {
            return true;
        }

        public void Pay(ICharacter character)
        {
            // Intentionally left blank
        }
    }
}