using System.Collections.Generic;

namespace ThirtyDayHero
{
    public static class Constants
    {
        public static readonly IReadOnlyDictionary<RankPriority, float> PRIORITY_WEIGHT = new Dictionary<RankPriority, float>()
        {
            {RankPriority.A, 9f},
            {RankPriority.B, 7f},
            {RankPriority.C, 6f},
            {RankPriority.D, 4.5f},
            {RankPriority.F, 3f},
        };
    }
}