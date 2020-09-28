using System;
using System.Collections.Generic;
using ThirtyDayHero.Armors.Definitions;
using ThirtyDayHero.Item.Weapons.Definitions;

namespace ThirtyDayHero.CharacterClasses
{
    public static class PlayerCharacterClassDefinitions
    {
        private static uint _nextId = 10000;
        public static uint NextId => ++_nextId;

        public static readonly IPlayerClass SOLDIER = new PlayerClass(
            NextId,
            "Soldier", "A former military recruit trained in basic martial weaponry.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.A, RankPriority.C, RankPriority.B, RankPriority.D, RankPriority.F, RankPriority.D),
            new StatMapIncrementor(RankPriority.A, RankPriority.C, RankPriority.B, RankPriority.D, RankPriority.F, RankPriority.C),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[]
                    {
                        new Ability(AbilityUtil.NextId,
                            "First Aid", "Simple field medicine.",
                            75,
                            new NoRequirements(),
                            new StatCost(StatType.STA, 15),
                            SingleAllyTargetCalculator.Instance,
                            new HealingEffect(
                                source => (uint) Math.Ceiling(source.Stats[StatType.INT] * 1f)))
                    }
                },
                {
                    2, new[]
                    {
                        new Ability(AbilityUtil.NextId,
                            "Cleave", "Sweep your sword through the enemy lines, dealing moderate damage to all.",
                            100,
                            new EquippedWeaponRequirement(WeaponType.Sword),
                            new StatCost(StatType.STA, 20),
                            AllEnemyTargetCalculator.Instance,
                            new DamageEffect(
                                DamageType.Normal,
                                source => (uint) Math.Ceiling(source.Stats[StatType.STR] * 0.75f)))
                    }
                }
            },
            WeaponType.Sword | WeaponType.Spear | WeaponType.Bow,
            ArmorType.Light | ArmorType.Medium,
            new EquipMapBuilder(
                new Dictionary<IWeapon, RankPriority>()
                {
                    {SwordDefinitions.ShortSword, RankPriority.B},
                    {SpearDefinitions.Spear, RankPriority.D},
                    {BowDefinitions.ShortBow, RankPriority.F}
                },
                new Dictionary<IArmor, RankPriority>()
                {
                    {LightDefinitions.Leather, RankPriority.B}
                },
                new Dictionary<IItem, RankPriority>(),
                new Dictionary<IItem, RankPriority>()));

        public static readonly IPlayerClass MAGE = new PlayerClass(
            NextId,
            "Mage", "An apprentice mage from the royal academy.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.F, RankPriority.C, RankPriority.D, RankPriority.B, RankPriority.B, RankPriority.C),
            new StatMapIncrementor(RankPriority.F, RankPriority.C, RankPriority.D, RankPriority.B, RankPriority.A, RankPriority.B),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[]
                    {
                        new Ability(AbilityUtil.NextId,
                            "Flare", "A quick burst of fire scorches your enemy.",
                            80,
                            new NoRequirements(),
                            new StatCost(StatType.STA, 15),
                            SingleEnemyTargetCalculator.Instance,
                            new DamageEffect(
                                DamageType.Fire,
                                source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.MAG) * 1.25f)))
                    }
                },
                {
                    2, new[]
                    {
                        new Ability(AbilityUtil.NextId,
                            "Nova", "A powerful burst of magic that damages all enemies.",
                            100,
                            new NoRequirements(),
                            new StatCost(StatType.STA, 60),
                            AllEnemyTargetCalculator.Instance,
                            new DamageEffect(
                                DamageType.Fire,
                                source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.MAG) * 3f)))
                    }
                }
            },
            WeaponType.Rod | WeaponType.Staff,
            ArmorType.Light,
            new EquipMapBuilder(
                new Dictionary<IWeapon, RankPriority>()
                {
                    {RodDefinitions.FireWand, RankPriority.A}
                },
                new Dictionary<IArmor, RankPriority>(),
                new Dictionary<IItem, RankPriority>(),
                new Dictionary<IItem, RankPriority>()));
    }
}