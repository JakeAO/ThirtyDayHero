using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Items
{
    public static class ConsumableDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ITEM_CONSUMABLE);

        #region HEALING POTIONS

        public static readonly IItem SmallHealingPotionItem = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Small Healing Potion",
            "Magical potion that restores a small amount of HP.",
            new[] {ItemAbilityDefinitions.DrinkSmallHealingPotion});

        public static readonly ItemDefinition SmallHealingPotion = new ItemDefinition(
            "assets/item/consumable/consumable_40.png",
            50,
            RarityCategory.Common,
            SmallHealingPotionItem);

        public static readonly IItem MediumHealingPotionItem = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Medium Healing Potion",
            "Magical potion that restores a medium amount of HP.",
            new[] {ItemAbilityDefinitions.DrinkMediumHealingPotion});

        public static readonly ItemDefinition MediumHealingPotion = new ItemDefinition(
            "assets/item/consumable/consumable_48.png",
            100,
            RarityCategory.Uncommon,
            MediumHealingPotionItem);

        public static readonly IItem LargeHealingPotionItem = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Large Healing Potion",
            "Magical potion that restores a large amount of HP.",
            new[] {ItemAbilityDefinitions.DrinkLargeHealingPotion});

        public static readonly ItemDefinition LargeHealingPotion = new ItemDefinition(
            "assets/item/consumable/consumable_98.png",
            200,
            RarityCategory.Uncommon,
            LargeHealingPotionItem);

        #endregion HEALING POTIONS

        #region STAMINA POTIONS

        public static readonly IItem SmallStaminaPotionItem = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Small Stamina Potion",
            "Magical potion that restores a small amount of STA.",
            new[] {ItemAbilityDefinitions.DrinkSmallStaminaPotion});

        public static readonly ItemDefinition SmallStaminaPotion = new ItemDefinition(
            "assets/item/consumable/consumable_41.png",
            50,
            RarityCategory.Common,
            SmallStaminaPotionItem);

        public static readonly IItem MediumStaminaPotionItem = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Medium Stamina Potion",
            "Magical potion that restores a medium amount of STA.",
            new[] {ItemAbilityDefinitions.DrinkMediumStaminaPotion});

        public static readonly ItemDefinition MediumStaminaPotion = new ItemDefinition(
            "assets/item/consumable/consumable_47.png",
            100,
            RarityCategory.Uncommon,
            MediumStaminaPotionItem);

        public static readonly IItem LargeStaminaPotionItem = new Item(
            IdTracker.Next,
            ItemType.Consumable,
            "Large Stamina Potion",
            "Magical potion that restores a large amount of STA.",
            new[] {ItemAbilityDefinitions.DrinkLargeStaminaPotion});

        public static readonly ItemDefinition LargeStaminaPotion = new ItemDefinition(
            "assets/item/consumable/consumable_99.png",
            200,
            RarityCategory.Uncommon,
            LargeStaminaPotionItem);

        #endregion STAMINA POTIONS

    }
}