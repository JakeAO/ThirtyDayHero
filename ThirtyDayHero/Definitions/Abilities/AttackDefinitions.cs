using SadPumpkin.Util.CombatEngine.Abilities;
using SadPumpkin.Util.CombatEngine.CostCalculators;
using SadPumpkin.Util.CombatEngine.EffectCalculators;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.RequirementCalculators;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.CombatEngine.TargetCalculators;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities
{
    // 'Attack' Abilities
    public static class AttackDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ABILITY_ATTACK);

        public static readonly IAbility Attack_STR_Fixed = new Ability(
            IdTracker.Next,
            "Attack", "Attack with your weapon.",
            100,
            NoRequirements.Instance,
            NoCost.Instance,
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Normal,
                source => source.Stats[StatType.STR]));

        public static readonly IAbility Attack_DEX_Fixed = new Ability(
            IdTracker.Next,
            "Attack", "Attack with your weapon.",
            100,
            NoRequirements.Instance,
            NoCost.Instance,
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Normal,
                source => source.Stats[StatType.DEX]));

        public static readonly IAbility Attack_MAG_Fixed = new Ability(
            IdTracker.Next,
            "Attack", "Attack with your weapon.",
            100,
            NoRequirements.Instance,
            NoCost.Instance,
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Normal,
                source => source.Stats[StatType.MAG]));
    }
}