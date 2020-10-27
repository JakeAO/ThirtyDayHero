using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.Abilities;
using SadPumpkin.Util.CombatEngine.CharacterClasses;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.CharacterClasses
{
    public static class MonsterDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.CLASS_MONSTER);

        public static readonly ICharacterClass OozeClass = new CharacterClass(
            IdTracker.Next,
            "Ooze",
            "Sticky and caustic. Oozes are dangerous in large numbers.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.B, RankPriority.C, RankPriority.B, RankPriority.F, RankPriority.C, RankPriority.F, StatMapBuilder.DEFAULT_TOTAL / 2),
            new StatMapIncrementor(RankPriority.B, RankPriority.C, RankPriority.B, RankPriority.F, RankPriority.C, RankPriority.F),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[] {AttackDefinitions.NewAttack(100, DamageType.Normal, StatType.STR, 0.3f, 0.7f)}
                },
                {
                    3, new[] {MonsterSkillDefinitions.MonsterSkill_Flop}
                }
            });

        public static readonly EnemyDefinition Ooze = new EnemyDefinition(
            "assets/enemy/ooze_1.png",
            RarityCategory.Common,
            NameGenerator.Monster,
            OozeClass);

        public static readonly ICharacterClass BlobClass = new CharacterClass(
            IdTracker.Next,
            "Blob",
            "Many adventurers have been smothered to death under blobs.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.A, RankPriority.F, RankPriority.A, RankPriority.F, RankPriority.F, RankPriority.F, StatMapBuilder.DEFAULT_TOTAL / 2),
            new StatMapIncrementor(RankPriority.C, RankPriority.C, RankPriority.C, RankPriority.C, RankPriority.C, RankPriority.C),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[] {AttackDefinitions.NewAttack(100, DamageType.Normal, StatType.STR, 0.3f, 0.7f)}
                },
                {
                    3, new[] {MonsterSkillDefinitions.MonsterSkill_Flop}
                }
            });

        public static readonly EnemyDefinition Blob = new EnemyDefinition(
            "assets/enemy/blob_1.png",
            RarityCategory.Common,
            NameGenerator.Monster,
            BlobClass);
    }
}