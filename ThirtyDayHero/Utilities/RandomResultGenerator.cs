using System;
using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Util.CombatEngine;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Utilities
{
    public static class RandomResultGenerator
    {
        private static readonly Random RANDOM = new Random();

        public static T Get<T>(IEnumerable<(T Item, double Chance)> itemsByChance)
        {
            if (itemsByChance == null)
                return default;

            double totalPriority = itemsByChance.Sum(x => x.Chance);
            if (totalPriority == 0)
                return default;

            double randVal = RANDOM.NextDouble() * totalPriority;
            foreach (var itemPriority in itemsByChance)
            {
                randVal -= itemPriority.Chance;
                if (randVal <= 0)
                {
                    return itemPriority.Item;
                }
            }

            return default;
        }

        public static T Get<T>(IReadOnlyDictionary<T, double> itemsByChance) =>
            Get(itemsByChance
                .Select(x => (x.Key, x.Value))
                .ToArray());

        public static T Get<T>(IReadOnlyDictionary<T, RankPriority> itemsByChance) =>
            Get(itemsByChance
                .Select(x => (x.Key, (double) Constants.PRIORITY_WEIGHT[x.Value])));
    }
}