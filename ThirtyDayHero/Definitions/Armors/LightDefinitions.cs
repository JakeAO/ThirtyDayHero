using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Armors
{
    public static class LightDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ARMOR_LIGHT);

        public static readonly IArmor TravelersTunicItem = new Armor(
            IdTracker.Next,
            "Traveler's Tunic", "Simple woven armor made for travelers.",
            ArmorType.Light,
            new Dictionary<DamageType, float>()
            {
                {DamageType.Normal, 0.98f}
            },
            null);

        public static readonly ItemDefinition TravelersTunic = new ItemDefinition(
            "assets/armor/light/chest_03.png",
            200,
            RarityCategory.Common,
            TravelersTunicItem);
    }
}