namespace ThirtyDayHero
{
    public class NoRequirements : IRequirementCalc
    {
        public static readonly NoRequirements Instance = new NoRequirements();
        
        public bool MeetsRequirement(ICharacterActor character)
        {
            return true;
        }
    }
}