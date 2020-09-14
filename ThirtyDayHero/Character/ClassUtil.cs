using System;

namespace ThirtyDayHero
{
    public static class ClassUtil
    {
        private static readonly Random RANDOM = new Random();

        private static uint _nextId = 10000;
        public static uint NextId => ++_nextId;

        public static ICharacter CreateCharacter(
            uint id, uint partyId,
            string name,
            ICharacterClass characterClass,
            uint level = 1)
        {
            IStatMap stats = characterClass.StartingStats.Generate(RANDOM);
            for (uint i = 1; i < level; i++)
            {
                stats.ModifyStat(StatType.LVL, 1);
                stats = characterClass.LevelUpStats.Increment(stats, RANDOM);
            }

            IEquipMap equipment = new EquipMap();

            return new Character(
                id, partyId,
                name,
                characterClass,
                stats,
                equipment);
        }
    }
}