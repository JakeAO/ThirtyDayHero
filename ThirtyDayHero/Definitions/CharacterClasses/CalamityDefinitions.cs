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
    public static class CalamityDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.CLASS_CALAMITY);

        public static readonly ICharacterClass DragonGodClass = new CharacterClass(
            IdTracker.Next,
            "Dragon God",
            "Elder dragons are revered as gods by many, and thought to be immortal.",
            new Dictionary<DamageType, float>()
            {
                {DamageType.Fire, 0.25f}
            },
            new StatMapBuilder(RankPriority.A, RankPriority.B, RankPriority.A, RankPriority.C, RankPriority.B, RankPriority.C),
            new StatMapIncrementor(RankPriority.A, RankPriority.B, RankPriority.A, RankPriority.C, RankPriority.B, RankPriority.C),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[]
                    {
                        AttackDefinitions.NewAttack(100, DamageType.Normal, StatType.STR, 0.3f, 0.7f),
                        MonsterSkillDefinitions.MonsterSkill_FlameBreath
                    }
                }
            });

        public static readonly EnemyDefinition DragonGod = new EnemyDefinition(
            "assets/enemy/dragongod_1.png",
            RarityCategory.Common,
            new NameGenerator(new[]
            {
                "Qondorth, The Firestarter",
                "Anam, Protector Of The Sky",
                "Mirvarth, Scourge Of The Yellow",
                "Candyt, The Tyrant",
                "Biarsyrth, The Evil One",
                "Noizos, The Death Giver",
                "Qerrar, Champion Of The Black",
                "Jezecram, The Strong Minded",
                "Urvecry, The Tall",
                "Diveine, Lord Of The Green",
            }),
            DragonGodClass);

        public static readonly ICharacterClass SludgeLordClass = new CharacterClass(
            IdTracker.Next,
            "Sludge Lord",
            "Tales speak of shapeless, primordial beings older than time.",
            new Dictionary<DamageType, float>()
            {
                {DamageType.Normal, 0.25f},
                {DamageType.Dark, 0.25f},
                {DamageType.Light, 0.25f}
            },
            new StatMapBuilder(RankPriority.B, RankPriority.C, RankPriority.A, RankPriority.F, RankPriority.B, RankPriority.F),
            new StatMapIncrementor(RankPriority.B, RankPriority.C, RankPriority.A, RankPriority.F, RankPriority.B, RankPriority.F),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[]
                    {
                        AttackDefinitions.NewAttack(100, DamageType.Normal, StatType.STR, 0.3f, 0.7f),
                        MonsterSkillDefinitions.MonsterSkill_Flop
                    }
                }
            });

        public static readonly EnemyDefinition SludgeLord = new EnemyDefinition(
            "assets/enemy/sludgelord_1.png",
            RarityCategory.Common,
            new NameGenerator(new[]
            {
                "Globbogool",
                "Gumgum",
                "Quartern",
                "Sir Grime",
                "Goohop",
                "Blibdilpulp",
                "Stench"
            }),
            SludgeLordClass);
    }
}