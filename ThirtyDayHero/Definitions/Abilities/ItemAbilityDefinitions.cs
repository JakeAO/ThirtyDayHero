using SadPumpkin.Util.CombatEngine.Abilities;
using SadPumpkin.Util.CombatEngine.CostCalculators;
using SadPumpkin.Util.CombatEngine.EffectCalculators;
using SadPumpkin.Util.CombatEngine.RequirementCalculators;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.CombatEngine.TargetCalculators;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities
{
    public static class ItemAbilityDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ABILITY_ITEM);

        public static readonly IAbility DrinkSmallHealingPotion = new Ability(
            IdTracker.Next,
            "Drink",
            "Consume the potion and restore 25% of your max HP.",
            75,
            NoRequirements.Instance,
            new DestroyThisItemCost("DrinkSmallHealingPotion"),
            SelfTargetCalculator.Instance,
            new HealingEffect(source => (uint) (source.Stats[StatType.HP_Max] * 0.25f),
                "[0.25x Max] HP Restore"));

        public static readonly IAbility DrinkMediumHealingPotion = new Ability(
            IdTracker.Next,
            "Drink",
            "Consume the potion and restore 50% of your max HP.",
            75,
            NoRequirements.Instance,
            new DestroyThisItemCost("DrinkMediumHealingPotion"),
            SelfTargetCalculator.Instance,
            new HealingEffect(source => (uint) (source.Stats[StatType.HP_Max] * 0.50f),
                "[0.5x Max] HP Restore"));

        public static readonly IAbility DrinkLargeHealingPotion = new Ability(
            IdTracker.Next,
            "Drink",
            "Consume the potion and restore 75% of your max HP.",
            75,
            NoRequirements.Instance,
            new DestroyThisItemCost("DrinkLargeHealingPotion"),
            SelfTargetCalculator.Instance,
            new HealingEffect(source => (uint) (source.Stats[StatType.HP_Max] * 0.75f),
                "[0.75x Max] HP Restore"));
        

        public static readonly IAbility DrinkSmallStaminaPotion = new Ability(
            IdTracker.Next,
            "Drink",
            "Consume the potion and restore 25% of your max STA.",
            75,
            NoRequirements.Instance,
            new DestroyThisItemCost("DrinkSmallStaminaPotion"),
            SelfTargetCalculator.Instance,
            new HealingEffect(source => (uint) (source.Stats[StatType.STA_Max] * 0.25f),
                "[0.25x Max] STA Restore"));

        public static readonly IAbility DrinkMediumStaminaPotion = new Ability(
            IdTracker.Next,
            "Drink",
            "Consume the potion and restore 50% of your max STA.",
            75,
            NoRequirements.Instance,
            new DestroyThisItemCost("DrinkMediumStaminaPotion"),
            SelfTargetCalculator.Instance,
            new HealingEffect(source => (uint) (source.Stats[StatType.STA_Max] * 0.50f),
                "[0.5x Max] STA Restore"));

        public static readonly IAbility DrinkLargeStaminaPotion = new Ability(
            IdTracker.Next,
            "Drink",
            "Consume the potion and restore 75% of your max STA.",
            75,
            NoRequirements.Instance,
            new DestroyThisItemCost("DrinkLargeStaminaPotion"),
            SelfTargetCalculator.Instance,
            new HealingEffect(source => (uint) (source.Stats[StatType.STA_Max] * 0.75f),
                "[0.75x Max] STA Restore"));
    }
}