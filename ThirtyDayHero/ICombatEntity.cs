﻿using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ICombatEntity
    {
        uint Id { get; }
        uint Party { get; }
        string Name { get; }

        float Initiative { get; }
        bool Alive { get; }
        
        IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ICharacter> allCharacters);
    }
}