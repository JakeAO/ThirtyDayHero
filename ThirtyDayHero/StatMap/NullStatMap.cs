namespace ThirtyDayHero
{
    public class NullStatMap : IStatMap
    {
        public static readonly NullStatMap Instance = new NullStatMap();

        private NullStatMap()
        {
        }
        
        public uint this[StatType statType] => 0u;
        public uint GetStat(StatType statType) => 0u;

        public void ModifyStat(StatType statType, int change)
        {
        }
    }
}