using SadPumpkin.Games.ThirtyDayHero.Core.Definitions;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.CharacterClasses;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Decorators
{
    public class EnemyDefinition : IIdTracked
    {
        public string ArtName { get; }
        public NameGenerator NameGenerator { get; }
        public ICharacterClass CharacterClass { get; }
        public RarityCategory Rarity { get; }

        public uint Id { get; }

        public EnemyDefinition(string artName, RarityCategory rarity, NameGenerator nameGenerator, ICharacterClass characterClass)
        {
            ArtName = artName;
            Rarity = rarity;
            NameGenerator = nameGenerator;
            CharacterClass = characterClass;
            Id = characterClass.Id;
        }
    }
}