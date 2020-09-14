using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IEffectCalc
    {
        void Apply(ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> targetCharacters);
    }
}