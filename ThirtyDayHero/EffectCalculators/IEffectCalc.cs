using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IEffectCalc
    {
        void Apply(ICombatEntity sourceEntity, IReadOnlyCollection<ICharacter> targetCharacters);
    }
}