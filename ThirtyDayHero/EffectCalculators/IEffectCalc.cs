using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IEffectCalc
    {
        void Apply(IInitiativeActor sourceEntity, IReadOnlyCollection<ICharacterActor> targetCharacters);
    }
}