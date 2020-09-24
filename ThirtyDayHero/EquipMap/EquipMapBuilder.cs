using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class EquipMapBuilder : IEquipMapBuilder
    {
        private readonly IReadOnlyDictionary<IWeapon, RankPriority> _startingWeapon = null;
        private readonly IReadOnlyDictionary<IArmor, RankPriority> _startingArmor = null;
        private readonly IReadOnlyDictionary<IItem, RankPriority> _startingItemA = null;
        private readonly IReadOnlyDictionary<IItem, RankPriority> _startingItemB = null;

        public EquipMapBuilder(
            IReadOnlyDictionary<IWeapon, RankPriority> startingWeapon,
            IReadOnlyDictionary<IArmor, RankPriority> startingArmor,
            IReadOnlyDictionary<IItem, RankPriority> startingItemA,
            IReadOnlyDictionary<IItem, RankPriority> startingItemB
        )
        {
            _startingWeapon = new Dictionary<IWeapon, RankPriority>(startingWeapon);
            _startingArmor = new Dictionary<IArmor, RankPriority>(startingArmor);
            _startingItemA = new Dictionary<IItem, RankPriority>(startingItemA);
            _startingItemB = new Dictionary<IItem, RankPriority>(startingItemB);
        }

        public IEquipMap Generate(Random random)
        {
            return new EquipMap(
                GetRandomItem(_startingWeapon, random),
                GetRandomItem(_startingArmor, random),
                GetRandomItem(_startingItemA, random),
                GetRandomItem(_startingItemB, random));
        }

        private static T GetRandomItem<T>(IReadOnlyDictionary<T, RankPriority> dict, Random rand) where T : IItem
        {
            double totalPriority = 0d;
            foreach (RankPriority statPriority in dict.Values)
            {
                totalPriority += Constants.PRIORITY_WEIGHT[statPriority];
            }

            double randVal = rand.NextDouble() * totalPriority;
            foreach (var priorityKvp in dict)
            {
                randVal -= Constants.PRIORITY_WEIGHT[priorityKvp.Value];
                if (randVal <= 0)
                {
                    return priorityKvp.Key;
                }
            }

            return default;
        }
    }
}