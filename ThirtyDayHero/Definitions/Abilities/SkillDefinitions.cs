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
    // 'Skill' Abilities
    public static class SkillDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ABILITY_SKILL);

        public static readonly IAbility Skill_FirstAid = new Ability(
            IdTracker.Next,
            "First Aid",
            "Simple field medicine.",
            75,
            new NoRequirements(),
            new StatCost(StatType.STA, 15),
            SingleAllyTargetCalculator.Instance,
            new HealingEffect(
                source => source.Stats[StatType.INT],
                "[INT] HP Restore"));

        public static readonly IAbility Skill_Cleave = new Ability(
            IdTracker.Next,
            "Cleave", "Sweep your blade through the enemy lines, dealing moderate damage to all but leaving you open.",
            150,
            new EquippedWeaponRequirement(WeaponType.Sword | WeaponType.Axe | WeaponType.GreatSword | WeaponType.GreatAxe),
            new StatCost(StatType.STA, 20),
            AllEnemyTargetCalculator.Instance,
            new DamageEffect(
                DamageType.Normal,
                source => (uint) Math.Ceiling(source.Stats[StatType.STR] * 0.50f),
                source => (uint) Math.Ceiling(source.Stats[StatType.STR] * 0.75f),
                "[0.5x STR]-[0.75x STR] Normal Damage"));
        
    }
}