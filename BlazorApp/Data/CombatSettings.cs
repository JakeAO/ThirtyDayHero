using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CharacterClasses;
using SadPumpkin.Util.CombatEngine.CharacterControllers;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public enum CombatDifficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2
    }

    public class CombatSettings
    {
        private static readonly Random RANDOM = new Random();

        public IReadOnlyCollection<ICharacterActor> Enemies { get; private set; }
        public ICharacterController AI { get; private set; }
        public uint PartyId { get; private set; }

        public static CombatSettings CreateFromEnemies(IReadOnlyCollection<ICharacterClass> enemyTypes, CombatDifficulty difficulty, PartyDataWrapper playerParty)
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
            foreach (ICharacterClass enemyClass in enemyTypes)
            {
                enemies.Add(
                    ClassUtil.CreateCharacter(
                        ClassUtil.NextId,
                        partyId,
                        NameGenerator.Monster.GetName(),
                        enemyClass));
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

            // Build Settings
            return new CombatSettings()
            {
                PartyId = partyId,
                AI = new RandomCharacterController(),
                Enemies = enemies
            };
        }

        public static CombatSettings CreateFromDifficulty(CombatDifficulty difficulty, PartyDataWrapper playerParty)
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

            List<ICharacterClass> enemyTypes = new List<ICharacterClass>(enemyCount);
            for (int i = 0; i < enemyCount; i++)
            {
                enemyTypes.Add(HackUtil.GetRandomMonsterClass());
            }

            return CreateFromEnemies(enemyTypes, difficulty, playerParty);
        }
    }
}