using System;
using System.Collections.Generic;

namespace ThirtyDayHero.CharacterClasses
{
    public static class MonsterCharacterClassDefinitions
    {
        private static uint _nextId = 15000;
        public static uint NextId => ++_nextId;

        public static readonly ICharacterClass OOZE = new CharacterClass(
            NextId,
            "Ooze", "It's both sticky AND dangerous. Great.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.B, RankPriority.C, RankPriority.B, RankPriority.F, RankPriority.C, RankPriority.F),
            new StatMapIncrementor(RankPriority.B, RankPriority.C, RankPriority.B, RankPriority.F, RankPriority.C, RankPriority.F),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[]
                    {
                        new Ability(AbilityUtil.NextId,
                            "Slam", string.Empty,
                            30u,
                            NoRequirements.Instance,
                            NoCost.Instance,
                            SingleEnemyTargetCalculator.Instance,
                            new DamageEffect(
                                DamageType.Normal,
                                source => source.Stats.GetStat(StatType.STR)))
                    }
                },
                {
                    3, new[]
                    {
                        new Ability(AbilityUtil.NextId,
                            "Flop", string.Empty,
                            50u,
                            NoRequirements.Instance,
                            NoCost.Instance,
                            SingleEnemyTargetCalculator.Instance,
                            new DamageEffect(
                                DamageType.Normal,
                                source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.STR) * 1.5f)))
                    }
                }
            });
    }
}