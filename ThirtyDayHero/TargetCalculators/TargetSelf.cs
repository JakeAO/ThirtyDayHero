using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class SelfTargetCalculator : ITargetCalc
    {
        public static readonly SelfTargetCalculator Instance = new SelfTargetCalculator();

        private SelfTargetCalculator()
        {

        }

        public bool CanTarget(ICharacter sourceCharacter, ICharacter targetCharacter)
        {
            return sourceCharacter.Id == targetCharacter.Id;
        }

        public IReadOnlyCollection<IReadOnlyCollection<ICharacter>> GetTargetOptions(ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> allCharacters)
        {
            return new[] {new[] {sourceCharacter}};
        }
    }
}