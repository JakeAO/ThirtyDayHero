using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class SingleEnemyTargetCalculator : ITargetCalc
    {
        public static readonly SingleEnemyTargetCalculator Instance = new SingleEnemyTargetCalculator();

        private SingleEnemyTargetCalculator()
        {

        }

        public bool CanTarget(ICharacter sourceCharacter, ICharacter targetCharacter)
        {
            return sourceCharacter.Party != targetCharacter.Party;
        }

        public IReadOnlyCollection<IReadOnlyCollection<ICharacter>> GetTargetOptions(ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> allCharacters)
        {
            List<IReadOnlyCollection<ICharacter>> targetOptions = new List<IReadOnlyCollection<ICharacter>>(allCharacters.Count);
            foreach (ICharacter targetCharacter in allCharacters)
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