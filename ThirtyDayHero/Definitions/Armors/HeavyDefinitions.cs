using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Armors
{
    public static class HeavyDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ARMOR_HEAVY);

        public static readonly IArmor ChainJacketItem = new Armor(
            IdTracker.Next,
            "Chain Jacket", "A jacket full of chainmail segments.",
            ArmorType.Heavy,
            new Dictionary<DamageType, float>()
            {
                {DamageType.Normal, 0.90f}
            },
            null);

        public static readonly ItemDefinition ChainJacket = new ItemDefinition(
            "assets/armor/heavy/chest_64.png",
            400,
            RarityCategory.Common,
            ChainJacketItem);
    }
}