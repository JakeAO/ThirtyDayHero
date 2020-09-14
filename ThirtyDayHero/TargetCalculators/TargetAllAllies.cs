using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class AllAllyTargetCalculator : ITargetCalc
    {
        public static readonly AllAllyTargetCalculator Instance = new AllAllyTargetCalculator();

        private AllAllyTargetCalculator()
        {
            
        }

        public bool CanTarget(ICharacter sourceCharacter, ICharacter targetCharacter)
        {
            return sourceCharacter.Party == targetCharacter.Party;
        }

        public IReadOnlyCollection<IReadOnlyCollection<ICharacter>> GetTargetOptions(ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> allCharacters)
        {
            List<ICharacter> allTargets = new List<ICharacter>(allCharacters.Count);
            foreach (ICharacter targetCharacter in allCharacters)
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