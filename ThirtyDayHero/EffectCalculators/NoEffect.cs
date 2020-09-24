using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class NoEffect : IEffectCalc
    {
        public static readonly NoEffect Instance = new NoEffect();
        
        public void Apply(ICombatEntity sourceEntity, IReadOnlyCollection<ICharacter> targetCharacters)
        {
            // Intentionally left blank
        }
    }
}