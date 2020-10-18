using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Items
{
    public static class ConsumableDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ITEM_CONSUMABLE);

        public static readonly IItem Potion_Healing_Small = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Small Healing Potion",
            "Magical potion that restores a small amount of HP.",
            new[] {ItemAbilityDefinitions.DrinkSmallHealingPotion});

        public static readonly IItem Potion_Healing_Medium = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Medium Healing Potion",
            "Magical potion that restores a medium amount of HP.",
            new[] {ItemAbilityDefinitions.DrinkMediumHealingPotion});

        public static readonly IItem Potion_Healing_Large = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Large Healing Potion",
            "Magical potion that restores a large amount of HP.",
            new[] {ItemAbilityDefinitions.DrinkMediumHealingPotion});

        public static readonly IItem Potion_Healing_Full = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Full Healing Potion",
            "Magical potion that restores all HP.",
            new[] {ItemAbilityDefinitions.DrinkFullHealingPotion});
    }
}