﻿using SadPumpkin.Games.ThirtyDayHero.Core.Definitions;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.CharacterClasses;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Decorators
{
    public class EnemyDefinition : IIdTracked
    {
        public NameGenerator NameGenerator { get; }
        public ICharacterClass CharacterClass { get; }

        public uint Id { get; }

        public EnemyDefinition(NameGenerator nameGenerator, ICharacterClass characterClass)
        {
            NameGenerator = nameGenerator;
            CharacterClass = characterClass;
            Id = characterClass.Id;
        }
    }
}