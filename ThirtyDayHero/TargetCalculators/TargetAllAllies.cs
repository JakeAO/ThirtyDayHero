using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class AllAllyTargetCalculator : ITargetCalc
    {
        public static readonly AllAllyTargetCalculator Instance = new AllAllyTargetCalculator();

        private AllAllyTargetCalculator()
        {
            
        }

        public bool CanTarget(ICharacterActor sourceCharacter, ICharacterActor targetCharacter)
        {
            return sourceCharacter.Party == targetCharacter.Party &&
                   targetCharacter.Alive;
        }

        public IReadOnlyCollection<IReadOnlyCollection<ICharacterActor>> GetTargetOptions(ICharacterActor sourceCharacter, IReadOnlyCollection<ICharacterActor> allCharacters)
        {
            List<ICharacterActor> allTargets = new List<ICharacterActor>(allCharacters.Count);
            foreach (ICharacterActor targetCharacter in allCharacters)
            {
                if (CanTarget(sourceCharacter, targetCharacter))
                {
                    allTargets.Add(targetCharacter);
                }
            }
            return new[] {allTargets};
        }
    }
}