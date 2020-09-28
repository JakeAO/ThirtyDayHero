﻿using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class AllEnemyTargetCalculator : ITargetCalc
    {
        public static readonly AllEnemyTargetCalculator Instance = new AllEnemyTargetCalculator();
        
        private AllEnemyTargetCalculator()
        {
            
        }
        
        public bool CanTarget(ICharacterActor sourceCharacter, ICharacterActor targetCharacter)
        {
            return sourceCharacter.Party != targetCharacter.Party &&
                   targetCharacter.Alive;
        }

        public IReadOnlyCollection<IReadOnlyCollection<ICharacterActor>> GetTargetOptions(ICharacterActor sourceCharacter, IReadOnlyCollection<ITargetableActor> possibleTargets)
        {
            List<ICharacterActor> allTargets = new List<ICharacterActor>(possibleTargets.Count);
            foreach (ITargetableActor possibleTarget in possibleTargets)
            {
                if (!(possibleTarget is ICharacterActor targetCharacter))
                    continue;

                if (CanTarget(sourceCharacter, targetCharacter))
                {
                    allTargets.Add(targetCharacter);
                }
            }
            return new[] {allTargets};
        }
    }
}