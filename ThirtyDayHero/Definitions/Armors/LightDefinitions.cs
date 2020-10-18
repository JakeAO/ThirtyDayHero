using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Armors
{
    public static class LightDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ARMOR_LIGHT);

        public static readonly IArmor Leather = new Armor(
            IdTracker.Next,
            "Leather Armor", "Simple armor made from overlapping leather.",
            ArmorType.Light,
            new Dictionary<DamageType, float>()
            {
                {DamageType.Normal, 0.98f}
            },
            null);
    }
}