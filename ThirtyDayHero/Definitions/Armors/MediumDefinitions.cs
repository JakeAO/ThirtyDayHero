using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Armors
{
    public static class MediumDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ARMOR_MEDIUM);

        public static readonly IArmor LeatherArmorItem = new Armor(
            IdTracker.Next,
            "Leather Armor", "Common armor made from layered leather.",
            ArmorType.Medium,
            new Dictionary<DamageType, float>()
            {
                {DamageType.Normal, 0.94f}
            },
            null);

        public static readonly ItemDefinition<IArmor> LeatherArmor = new ItemDefinition<IArmor>(
            "assets/armor/medium/chest_22.png",
            300,
            RarityCategory.Common,
            LeatherArmorItem);
    }
}