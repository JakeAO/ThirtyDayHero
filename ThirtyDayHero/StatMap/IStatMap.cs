namespace ThirtyDayHero
{
    public interface IStatMap
    {
        uint this[StatType statType] { get; }
        
        uint GetStat(StatType statType);

        void ModifyStat(StatType statType, int change);
    }
}