using SadPumpkin.Games.ThirtyDayHero.Core.Definitions;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.CharacterClasses;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Decorators
{
    public class PlayerClassDefinition : IIdTracked
    {
        public NameGenerator NameGenerator { get; }
        public IPlayerClass PlayerClass { get; }
        public RarityCategory Rarity { get; }
        
        public uint Id { get; }

        public PlayerClassDefinition(RarityCategory rarity, NameGenerator nameGenerator, IPlayerClass playerClass)
        {
            Rarity = rarity;
            NameGenerator = nameGenerator;
            PlayerClass = playerClass;
            Id = playerClass.Id;
        }
    }
}