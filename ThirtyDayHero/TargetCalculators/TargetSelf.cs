using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class SelfTargetCalculator : ITargetCalc
    {
        public static readonly SelfTargetCalculator Instance = new SelfTargetCalculator();

        private SelfTargetCalculator()
        {

        }

        public bool CanTarget(ICharacterActor sourceCharacter, ICharacterActor targetCharacter)
        {
            return sourceCharacter.Id == targetCharacter.Id;
        }

        public IReadOnlyCollection<IReadOnlyCollection<ICharacterActor>> GetTargetOptions(ICharacterActor sourceCharacter, IReadOnlyCollection<ITargetableActor> possibleTargets)
        {
            return new[] {new[] {sourceCharacter}};
        }
    }
}