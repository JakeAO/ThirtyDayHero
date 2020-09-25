using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class NoEffect : IEffectCalc
    {
        public static readonly NoEffect Instance = new NoEffect();
        
        public void Apply(IInitiativeActor sourceEntity, IReadOnlyCollection<ICharacterActor> targetCharacters)
        {
            // Intentionally left blank
        }
    }
}