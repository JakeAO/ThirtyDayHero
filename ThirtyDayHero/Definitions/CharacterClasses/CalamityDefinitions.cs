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

        public static readonly EnemyDefinition DRAGONGOD = new EnemyDefinition(
            new NameGenerator(new[] {"The Dragon King"}),
            new CharacterClass(
                IdTracker.Next,
                "The Dragon King",
                "Dragon King description",
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
                            AttackDefinitions.Attack_STR_Fixed,
                            AttackDefinitions.Attack_DEX_Fixed,
                            MonsterSkillDefinitions.MonsterSkill_FlameBreath
                        }
                    }
                }));
    }
}