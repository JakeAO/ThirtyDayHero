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
    // 'Spell' Abilities
    public static class SpellDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ABILITY_SPELL);

        public static readonly IAbility Spell_Ember = new Ability(
            IdTracker.Next,
            "Ember",
            "Launch a small fireball at an enemy.",
            100,
            NoRequirements.Instance,
            new StatCost(StatType.STA, 5),
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Fire,
                source => source.Stats[StatType.MAG],
                "[MAG] Fire Damage"));

        public static readonly IAbility Spell_Snowball = new Ability(
            IdTracker.Next,
            "Snowball",
            "Hurl a clod of snow and ice at an enemy.",
            100,
            NoRequirements.Instance,
            new StatCost(StatType.STA, 5),
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Water,
                source => source.Stats[StatType.MAG],
                "[MAG] Water Damage"));

        public static readonly IAbility Spell_Spike = new Ability(
            IdTracker.Next,
            "Spike",
            "Cause a stone spike to erupt from beneath an enemy.",
            100,
            NoRequirements.Instance,
            new StatCost(StatType.STA, 5),
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Stone,
                source => source.Stats[StatType.MAG],
                "[MAG] Stone Damage"));

        public static readonly IAbility Spell_Gale = new Ability(
            IdTracker.Next,
            "Gale",
            "Blast an enemy with a gust of biting wind.",
            100,
            NoRequirements.Instance,
            new StatCost(StatType.STA, 5),
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Wind,
                source => source.Stats[StatType.MAG],
                "[MAG] Wind Damage"));

        public static readonly IAbility Spell_Flare = new Ability(
            IdTracker.Next,
            "Flare",
            "A quick burst of fire scorches your enemy.",
            75,
            new NoRequirements(),
            new StatCost(StatType.STA, 15),
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Fire,
                source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.MAG) * 1.25f),
                "[1.25x MAG] Fire Damage"));


        public static readonly IAbility Spell_Nova = new Ability(
            IdTracker.Next,
            "Nova",
            "A powerful burst of magic that wreaks havoc on a single enemy.",
            100,
            new NoRequirements(),
            new StatCost(StatType.STA, 60),
            SingleEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Fire,
                source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.MAG) * 3f),
                "[3x MAG] Fire Damage"));

    }
}