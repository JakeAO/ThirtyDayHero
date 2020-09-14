using System;
using System.Collections.Generic;

namespace ThirtyDayHero.CharacterClasses
{
    public static class PlayerCharacterClassDefinitions
    {
        private static uint _nextId = 10000;
        public static uint NextId => ++_nextId;

        public static readonly ICharacterClass SOLDIER = new CharacterClass(
            NextId,
            "Soldier", "A former military recruit trained in basic martial weaponry.",
            new StatMapBuilder(RankPriority.A, RankPriority.C, RankPriority.B, RankPriority.D, RankPriority.F, RankPriority.D),
            new StatMapIncrementor(RankPriority.A, RankPriority.C, RankPriority.B, RankPriority.D, RankPriority.F, RankPriority.C),
            WeaponType.Sword | WeaponType.Spear | WeaponType.Bow,
            ArmorType.Light | ArmorType.Medium,
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    2, new[]
                    {
                        new Ability(AbilityUtil.NextId,
                            "Cleave", "Sweep your sword through the enemy lines, dealing moderate damage to all.",
                            80,
                            new EquippedWeaponRequirement(WeaponType.Sword),
                            new StatCost(StatType.STA, 20),
                            AllEnemyTargetCalculator.Instance,
                            new DamageEffect(
                                DamageType.Normal,
                                source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.STR) * 0.75f)))
                    }
                }
            });
    }
}