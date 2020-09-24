using System.Collections.Generic;

namespace ThirtyDayHero.Armors.Definitions
{
    public static class LightDefinitions
    {
        private static uint _nextId = 120000 + (int) ArmorType.Light * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IArmor Leather = new Armor(
            NextId,
            "Leather Armor", "Simple armor made from overlapping leather.",
            ArmorType.Light,
            new Dictionary<DamageType, float>()
            {
                {DamageType.Normal, 0.98f}
            },
            null);
    }
}