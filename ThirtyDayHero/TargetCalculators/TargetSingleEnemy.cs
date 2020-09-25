﻿using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class SingleEnemyTargetCalculator : ITargetCalc
    {
        public static readonly SingleEnemyTargetCalculator Instance = new SingleEnemyTargetCalculator();

        private SingleEnemyTargetCalculator()
        {

        }

        public bool CanTarget(ICharacterActor sourceCharacter, ICharacterActor targetCharacter)
        {
            return sourceCharacter.Party != targetCharacter.Party &&
                   targetCharacter.Alive;
        }

        public IReadOnlyCollection<IReadOnlyCollection<ICharacterActor>> GetTargetOptions(ICharacterActor sourceCharacter, IReadOnlyCollection<ICharacterActor> allCharacters)
        {
            List<IReadOnlyCollection<ICharacterActor>> targetOptions = new List<IReadOnlyCollection<ICharacterActor>>(allCharacters.Count);
            foreach (ICharacterActor targetCharacter in allCharacters)
            {
                if (CanTarget(sourceCharacter, targetCharacter))
                {
                    targetOptions.Add(new[] {targetCharacter});
                }
            }
            return targetOptions;
        }
    }
}