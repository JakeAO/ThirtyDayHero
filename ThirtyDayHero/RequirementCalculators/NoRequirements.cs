namespace ThirtyDayHero
{
    public class NoRequirements : IRequirementCalc
    {
        public static readonly NoRequirements Instance = new NoRequirements();
        
        public bool MeetsRequirement(ICharacter character)
        {
            return true;
        }
    }
}