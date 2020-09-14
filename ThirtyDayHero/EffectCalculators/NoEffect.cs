using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class NoEffect : IEffectCalc
    {
        public static readonly NoEffect Instance = new NoEffect();
        
        public void Apply(ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> targetCharacters)
        {
            // Intentionally left blank
        }
    }
}