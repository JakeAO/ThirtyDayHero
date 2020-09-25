using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class StaminaEffect : IEffectCalc
    {
        public Func<ICharacterActor, int> StaminaCalculation { get; }

        public StaminaEffect(Func<ICharacterActor, int> staminaCalculation)
        {
            StaminaCalculation = staminaCalculation;
        }

        public void Apply(IInitiativeActor sourceEntity, IReadOnlyCollection<ICharacterActor> targetCharacters)
        {
            if (sourceEntity is ICharacterActor sourceCharacter)
            {
                int stamina = StaminaCalculation(sourceCharacter);
                foreach (ICharacterActor targetCharacter in targetCharacters)
                {
                    targetCharacter.Stats.ModifyStat(StatType.STA, stamina);
                }
            }
        }
    }
}