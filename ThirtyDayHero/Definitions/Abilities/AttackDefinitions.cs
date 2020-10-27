using System;
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

        public static IAbility NewAttack(uint speed, DamageType damageType, StatType statType, float minMultiplier, float maxMultiplier) =>
            new Ability(
                IdTracker.Next,
                "Attack", "Attack with the equipped weapon.",
                speed,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                Math.Abs(minMultiplier - maxMultiplier) < 0.01f
                    ? new DamageEffect(
                        damageType,
                        source => (uint) Math.Round(source.Stats[statType] * minMultiplier),
                        $"[{minMultiplier}] x {statType} Damage")
                    : new DamageEffect(
                        damageType,
                        source => (uint) Math.Round(source.Stats[statType] * minMultiplier),
                        source => (uint) Math.Round(source.Stats[statType] * maxMultiplier),
                        $"[{minMultiplier}-{maxMultiplier}] x {statType} Damage"));
    }
}