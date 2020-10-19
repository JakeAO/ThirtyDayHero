﻿using System.Collections.Generic;
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

        public static readonly ICharacterClass Ooze = new CharacterClass(
            IdTracker.Next,
            "Ooze",
            "It's both sticky AND dangerous. Great.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.B, RankPriority.C, RankPriority.B, RankPriority.F, RankPriority.C, RankPriority.F, StatMapBuilder.DEFAULT_TOTAL_MONSTER),
            new StatMapIncrementor(RankPriority.B, RankPriority.C, RankPriority.B, RankPriority.F, RankPriority.C, RankPriority.F),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[] {AttackDefinitions.Attack_STR_Fixed}
                },
                {
                    3, new[] {MonsterSkillDefinitions.MonsterSkill_Flop}
                }
            });

        public static readonly ICharacterClass Blob = new CharacterClass(
            IdTracker.Next,
            "Blob",
            "It's like an Ooze, just less sticky.",
            new Dictionary<DamageType, float>(),
            new StatMapBuilder(RankPriority.A, RankPriority.F, RankPriority.A, RankPriority.F, RankPriority.F, RankPriority.F, StatMapBuilder.DEFAULT_TOTAL_MONSTER),
            new StatMapIncrementor(RankPriority.C, RankPriority.C, RankPriority.C, RankPriority.C, RankPriority.C, RankPriority.C),
            new Dictionary<uint, IReadOnlyCollection<IAbility>>()
            {
                {
                    1, new[] {AttackDefinitions.Attack_STR_Fixed}
                },
                {
                    3, new[] {MonsterSkillDefinitions.MonsterSkill_Flop}
                }
            });
    }
}