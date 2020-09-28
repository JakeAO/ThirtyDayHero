﻿using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class SingleAllyTargetCalculator : ITargetCalc
    {
        public static readonly SingleAllyTargetCalculator Instance = new SingleAllyTargetCalculator();
        
        private SingleAllyTargetCalculator()
        {
            
        }

        public bool CanTarget(ICharacterActor sourceCharacter, ICharacterActor targetCharacter)
        {
            return sourceCharacter.Party == targetCharacter.Party &&
                   targetCharacter.Alive;
        }

        public IReadOnlyCollection<IReadOnlyCollection<ICharacterActor>> GetTargetOptions(ICharacterActor sourceCharacter, IReadOnlyCollection<ITargetableActor> possibleTargets)
        {
            List<IReadOnlyCollection<ICharacterActor>> targetOptions = new List<IReadOnlyCollection<ICharacterActor>>(possibleTargets.Count);
            foreach (ITargetableActor possibleTarget in possibleTargets)
            {
                if (!(possibleTarget is ICharacterActor targetCharacter))
                    continue;
                
                if (CanTarget(sourceCharacter, targetCharacter))
                {
                    targetOptions.Add(new[] {targetCharacter});
                }
            }
            return targetOptions;
        }
    }
}