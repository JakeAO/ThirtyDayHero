using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ITargetCalc
    {
        bool CanTarget(ICharacter sourceCharacter, ICharacter targetCharacter);
        IReadOnlyCollection<IReadOnlyCollection<ICharacter>> GetTargetOptions(ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> allCharacters);
    }
}