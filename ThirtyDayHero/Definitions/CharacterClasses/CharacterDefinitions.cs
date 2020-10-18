using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Armors;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.Abilities;
using SadPumpkin.Util.CombatEngine.CharacterClasses;
using SadPumpkin.Util.CombatEngine.EquipMap;
using SadPumpkin.Util.CombatEngine.Item;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.CharacterClasses
{
    public static class CharacterDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.CLASS_PLAYER);

        public static readonly IPlayerClass Soldier = new PlayerClass(
            IdTracker.Next,
            "Soldier", "A former military recruit trained in basic martial weaponry.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.A, RankPriority.C, RankPriority.B, RankPriority.D, RankPriority.F, RankPriority.D),
            new StatMapIncrementor(RankPriority.A, RankPriority.C, RankPriority.B, RankPriority.D, RankPriority.F, RankPriority.C),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {1, new[] {SkillDefinitions.Skill_FirstAid}},
                {3, new[] {SkillDefinitions.Skill_Cleave}}
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

        public static readonly IPlayerClass Mage = new PlayerClass(
            IdTracker.Next,
            "Mage", "An apprentice mage from the royal academy.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.F, RankPriority.C, RankPriority.D, RankPriority.B, RankPriority.B, RankPriority.C),
            new StatMapIncrementor(RankPriority.F, RankPriority.C, RankPriority.D, RankPriority.B, RankPriority.A, RankPriority.B),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {1, new[] {SpellDefinitions.Spell_Flare}},
                {3, new[] {SpellDefinitions.Spell_Nova}}
            },
            WeaponType.Rod | WeaponType.Staff,
            ArmorType.Light,
            new EquipMapBuilder(
                new Dictionary<IWeapon, RankPriority>()
                {
                    {RodDefinitions.FireWand, RankPriority.A},
                    {RodDefinitions.IceWand, RankPriority.A},
                    {RodDefinitions.StoneWand, RankPriority.A},
                    {RodDefinitions.WindWand, RankPriority.A}
                },
                new Dictionary<IArmor, RankPriority>(),
                new Dictionary<IItem, RankPriority>(),
                new Dictionary<IItem, RankPriority>()));

        public static readonly IPlayerClass Ranger = new PlayerClass(
            IdTracker.Next,
            "Ranger", "A survivalist from the wilds.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.B, RankPriority.A, RankPriority.B, RankPriority.D, RankPriority.C, RankPriority.F),
            new StatMapIncrementor(RankPriority.C, RankPriority.A, RankPriority.C, RankPriority.F, RankPriority.C, RankPriority.F),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {1, new[] {SkillDefinitions.Skill_FirstAid}},
            },
            WeaponType.Axe | WeaponType.Bow,
            ArmorType.Light,
            new EquipMapBuilder(
                new Dictionary<IWeapon, RankPriority>()
                {
                    {BowDefinitions.ShortBow, RankPriority.A},
                    {AxeDefinitions.Hatchet, RankPriority.C}
                },
                new Dictionary<IArmor, RankPriority>()
                {
                    {LightDefinitions.Leather, RankPriority.B}
                },
                new Dictionary<IItem, RankPriority>(),
                new Dictionary<IItem, RankPriority>()));
    }
}