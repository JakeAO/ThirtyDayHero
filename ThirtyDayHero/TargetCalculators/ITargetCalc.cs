using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ITargetCalc
    {
        bool CanTarget(ICharacterActor sourceCharacter, ICharacterActor targetCharacter);
        IReadOnlyCollection<IReadOnlyCollection<ICharacterActor>> GetTargetOptions(ICharacterActor sourceCharacter, IReadOnlyCollection<ICharacterActor> allCharacters);
    }
}