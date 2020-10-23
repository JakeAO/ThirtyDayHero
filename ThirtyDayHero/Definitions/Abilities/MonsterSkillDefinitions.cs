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
    // Monster-Exclusive Abilities
    public class MonsterSkillDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ABILITY_MONSTER);

        public static readonly IAbility MonsterSkill_Flop = new Ability(
            IdTracker.Next,
            "Flop",
            string.Empty,
            100u,
            NoRequirements.Instance,
            new StatCost(StatType.STA, 5),
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Normal,
                source => (uint) Math.Ceiling(source.Stats[StatType.STR] * 1.5f),
                "[1.5x STR] Normal Damage"));

        public static readonly IAbility MonsterSkill_FlameBreath = new Ability(
            IdTracker.Next,
            "Flame Breath",
            string.Empty,
            120u,
            NoRequirements.Instance,
            NoCost.Instance,
            AllEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Fire,
                source => (uint) (source.Stats[StatType.MAG] * 0.33f),
                "[0.33x MAG] Fire Damage"));
    }
}