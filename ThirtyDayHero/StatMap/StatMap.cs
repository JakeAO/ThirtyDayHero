using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class StatMap : IStatMap
    {
        private static readonly IReadOnlyDictionary<StatType, uint> DEFAULT_STATS = new Dictionary<StatType, uint>()
        {
            {StatType.HP, 100},
            {StatType.HP_Max, 100},
            {StatType.STA, 100},
            {StatType.STA_Max, 100},
            {StatType.STR, 10},
            {StatType.DEX, 10},
            {StatType.CON, 10},
            {StatType.INT, 10},
            {StatType.MAG, 10},
            {StatType.CHA, 10},
            {StatType.LVL, 1},
            {StatType.EXP, 0},
        };
        
        private readonly Dictionary<StatType, uint> _stats = new Dictionary<StatType, uint>();

        public StatMap() : this(DEFAULT_STATS)
        {
            
        }

        public StatMap(IReadOnlyDictionary<StatType, uint> startingStats)
        {
            foreach (var enumValue in Enum.GetValues(typeof(StatType)))
            {
                StatType statType = (StatType) enumValue;
                if (statType == StatType.Invalid)
                    continue;

                if (!startingStats.TryGetValue(statType, out uint statValue))
                    if (!DEFAULT_STATS.TryGetValue(statType, out statValue))
                        throw new ArgumentOutOfRangeException($"StatType \"{statType}\" unsupported by StatMap!");

                _stats[statType] = statValue;
            }
        }

        public uint this[StatType statType] => GetStat(statType);

        public uint GetStat(StatType statType)
        {
            if (!_stats.TryGetValue(statType, out uint statValue))
                if (!DEFAULT_STATS.TryGetValue(statType, out statValue))
                    throw new ArgumentOutOfRangeException($"StatType \"{statType}\" unsupported by StatMap!");

            return statValue;
        }

        public void ModifyStat(StatType statType, int change)
        {
            uint currentValue = GetStat(statType);

            // Upper Clamp to Max (HP, STA)
            switch (statType)
            {
                case StatType.HP:
                    uint maxHp = GetStat(StatType.HP_Max);
                    currentValue = (uint) Math.Clamp(currentValue + change, 0u, maxHp);
                    break;
                case StatType.STA:
                    uint maxSta = GetStat(StatType.STA_Max);
                    currentValue = (uint) Math.Clamp(currentValue + change, 0u, maxSta);
                    break;
                case StatType.EXP:
                    currentValue = (uint) (currentValue + change);
                    uint levelsUp = currentValue / 100;
                    if (levelsUp > 0)
                    {
                        ModifyStat(StatType.LVL, (int) levelsUp);
                        currentValue %= 100;
                    }
                    break;
                default:
                    currentValue = (uint) Math.Max(currentValue + change, 0u);
                    break;
            }

            _stats[statType] = currentValue;
        }
    }
}