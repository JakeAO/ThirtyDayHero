using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Utilities;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CharacterControllers;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class CombatSettings
    {
        private static readonly Random RANDOM = new Random();

        public IReadOnlyCollection<ICharacterActor> Enemies { get; private set; }
        public ICharacterController AI { get; private set; }

        public static CombatSettings CreateFromEnemies(IReadOnlyCollection<ICharacterActor> enemies)
        {
            // Build Settings
            return new CombatSettings()
            {
                AI = new RandomCharacterController(),
                Enemies = enemies
            };
        }

        public static CombatSettings CreateFromEnemyTypes(IReadOnlyCollection<EnemyDefinition> enemyTypes, CombatDifficulty difficulty, PartyDataWrapper playerParty)
        {
            uint GetStatTotal(IReadOnlyCollection<ICharacterActor> actors)
            {
                uint total = 0u;
                foreach (ICharacterActor actor in actors)
                {
                    for (StatType statType = StatType.STR; statType <= StatType.CHA; statType++)
                    {
                        total += actor.Stats[statType];
                    }
                }

                return total;
            }

            uint GetStatTarget(uint totalStats, CombatDifficulty diff)
            {
                switch (diff)
                {
                    case CombatDifficulty.Normal:
                        return (uint) (totalStats * 0.75f);
                    case CombatDifficulty.Hard:
                        return (uint) (totalStats * 1f);
                    case CombatDifficulty.Easy:
                    default:
                        return (uint) (totalStats * 0.5f);
                }
            }

            uint partyId = (uint) Guid.NewGuid().GetHashCode();

            // Generate Enemies
            List<Character> enemies = new List<Character>(enemyTypes.Count);
            foreach (EnemyDefinition enemyDefinition in enemyTypes)
            {
                var newEnemy = ClassUtil.CreateCharacter(
                    ClassUtil.NextId,
                    partyId,
                    enemyDefinition.NameGenerator.GetName(),
                    enemyDefinition.CharacterClass);
                enemies.Add(newEnemy);
            }

            // Scale Enemies for Difficulty
            uint totalPartyStats = GetStatTotal(playerParty.Characters);
            uint targetTotalEnemyStats = GetStatTarget(totalPartyStats, difficulty);
            uint totalEnemyStats = GetStatTotal(enemies);
            while (totalEnemyStats < targetTotalEnemyStats)
            {
                Character enemy = enemies[RANDOM.Next(enemies.Count)];
                enemy.Stats = enemy.Class.LevelUpStats.Increment(enemy.Stats, RANDOM);
                enemy.Stats.ModifyStat(StatType.LVL, 1);

                totalEnemyStats = GetStatTotal(enemies);
            }

            return CreateFromEnemies(enemies);
        }

        public static CombatSettings CreateFromEnemyGroup(EnemyGroup enemyGroup, CombatDifficulty difficulty, PartyDataWrapper playerParty)
        {
            int enemyCount = 1;
            switch (difficulty)
            {
                case CombatDifficulty.Easy:
                    enemyCount = Math.Max(1, RANDOM.Next(1, playerParty.Characters.Count));
                    break;
                case CombatDifficulty.Normal:
                    enemyCount = Math.Max(2, RANDOM.Next(playerParty.Characters.Count - 1, playerParty.Characters.Count + 1));
                    break;
                case CombatDifficulty.Hard:
                    enemyCount = Math.Max(2, RANDOM.Next(playerParty.Characters.Count - 1, playerParty.Characters.Count + 2));
                    break;
            }

            List<EnemyDefinition> enemyTypes = new List<EnemyDefinition>(enemyCount);
            for (int i = 0; i < enemyCount; i++)
            {
                enemyTypes.Add(RandomResultGenerator.Get(enemyGroup.EnemyTypesByRarity));
            }

            return CreateFromEnemyTypes(enemyTypes, difficulty, playerParty);
        }

        public static CombatSettings CreateFromDifficulty(CombatDifficulty difficulty, PartyDataWrapper playerParty)
        {
            return CreateFromEnemyGroup(HackUtil.GetRandomEnemyGroup(), difficulty, playerParty);
        }
    }
}