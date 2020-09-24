namespace ThirtyDayHero
{
    public interface IStatMap
    {
        uint GetStat(StatType statType);

        void ModifyStat(StatType statType, int change);
    }
}