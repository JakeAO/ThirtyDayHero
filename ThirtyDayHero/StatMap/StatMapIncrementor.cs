using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class StatMapIncrementor : IStatMapIncrementor
    {
        public const uint DEFAULT_TOTAL = 12;
        public const uint DEFAULT_MIN = 0;

        private const uint HP_INCREASE_PER_CON = 10;
        private const uint STA_INCREASE_PER_WEIGHTED_STAT = 10;

        private static readonly IReadOnlyDictionary<RankPriority, float> PRIORITY_WEIGHT = new Dictionary<RankPriority, float>()
        {
            {RankPriority.A, 9f},
            {RankPriority.B, 7f},
            {RankPriority.C, 6f},
            {RankPriority.D, 4.5f},
            {RankPriority.F, 3f},
        };

        private readonly uint _statTotal;
        private readonly uint _statMin;
        private readonly IReadOnlyDictionary<StatType, RankPriority> _statPriorities = null;

        public StatMapIncrementor(
            RankPriority strPri,
            RankPriority dexPri,
            RankPriority conPri,
            RankPriority intPri,
            RankPriority magPri,
            RankPriority chaPri,
            uint statTotal = DEFAULT_TOTAL,
            uint statMin = DEFAULT_MIN)
        {
            _statPriorities = new Dictionary<StatType, RankPriority>()
            {
                {StatType.STR, strPri},
                {StatType.DEX, dexPri},
                {StatType.CON, conPri},
                {StatType.INT, intPri},
                {StatType.MAG, magPri},
                {StatType.CHA, chaPri},
            };
            _statTotal = statTotal - statMin * 6;
            _statMin = statMin;
        }

        public IStatMap Increment(IStatMap statMap, Random random)
        {
            Dictionary<StatType, uint> statChanges = new Dictionary<StatType, uint>()
            {
                {StatType.STR, _statMin},
                {StatType.DEX, _statMin},
                {StatType.CON, _statMin},
                {StatType.INT, _statMin},
                {StatType.MAG, _statMin},
                {StatType.CHA, _statMin},
            };

            double totalPriority = 0d;
            foreach (RankPriority statPriority in _statPriorities.Values)
            {
                totalPriority += PRIORITY_WEIGHT[statPriority];
            }

            for (int i = 0; i < _statTotal; i++)
            {
                double randVal = random.NextDouble() * totalPriority;
                foreach (var priorityKvp in _statPriorities)
                {
                    randVal -= PRIORITY_WEIGHT[priorityKvp.Value];
                    if (randVal <= 0)
                    {
                        statChanges[priorityKvp.Key] += 1;
                        break;
                    }
                }
            }

            uint hpGain = HP_INCREASE_PER_CON * statChanges[StatType.CON];
            uint maxAtkStat = Math.Max(Math.Max(statChanges[StatType.STR], statChanges[StatType.DEX]), statChanges[StatType.MAG]);
            uint maxAtkStatAvgWithInt = (maxAtkStat + statChanges[StatType.INT]) / 2;
            uint staGain = STA_INCREASE_PER_WEIGHTED_STAT * maxAtkStatAvgWithInt;

            statChanges[StatType.HP_Max] = statChanges[StatType.HP] = hpGain;
            statChanges[StatType.STA_Max] = statChanges[StatType.STA] = staGain;

            Dictionary<StatType, uint> newStats = new Dictionary<StatType, uint>();
            foreach (var enumValue in Enum.GetValues(typeof(StatType)))
            {
                StatType statType = (StatType) enumValue;
                if (statType == StatType.Invalid)
                    continue;
                newStats[statType] = statMap.GetStat(statType);
                if (statChanges.TryGetValue(statType, out uint statChange))
                    newStats[statType] += statChange;
            }

            return new StatMap(newStats);
        }
    }
}