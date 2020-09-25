namespace ThirtyDayHero
{
    public class CriticalHealthRequirement : IRequirementCalc
    {
        public static readonly CriticalHealthRequirement Instance = new CriticalHealthRequirement();
        
        public bool MeetsRequirement(ICharacterActor character)
        {
            return character.Stats.GetStat(StatType.HP) <= character.Stats.GetStat(StatType.HP_Max) * 0.2f;
        }
    }
}