using System.Collections.Generic;

namespace ThirtyDayHero.CharacterClasses
{
    public class CharacterClass : ICharacterClass
    {
        public uint Id { get; }
        public string Name { get; }
        public string Desc { get; }
        public IReadOnlyDictionary<DamageType, float> IntrinsicDamageModification { get; }
        public IStatMapBuilder StartingStats { get; }
        public IStatMapIncrementor LevelUpStats { get; }
        public IReadOnlyDictionary<uint, IReadOnlyCollection<IAbility>> AbilitiesPerLevel { get; }

        public CharacterClass(
            uint id,
            string name,
            string desc,
            IReadOnlyDictionary<DamageType, float> intrinsicDamageModification,
            IStatMapBuilder startingStats,
            IStatMapIncrementor levelUpStats,
            IReadOnlyDictionary<uint, IReadOnlyCollection<IAbility>> abilitiesPerLevel)
        {
            Id = id;
            Name = name;
            Desc = desc;
            StartingStats = startingStats;
            LevelUpStats = levelUpStats;
            AbilitiesPerLevel = abilitiesPerLevel;
            IntrinsicDamageModification = new Dictionary<DamageType, float>(intrinsicDamageModification);
        }

        public IReadOnlyCollection<IAbility> GetAllAbilities(uint level)
        {
            List<IAbility> abilities = new List<IAbility>(10);

            for (uint i = 0; i <= level; i++)
            {
                if (AbilitiesPerLevel.TryGetValue(i, out var abilitiesForLevel))
                {
                    abilities.AddRange(abilitiesForLevel);
                }
            }

            return abilities;
        }
    }
}