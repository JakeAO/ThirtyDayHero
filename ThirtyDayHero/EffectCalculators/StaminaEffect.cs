using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class StaminaEffect : IEffectCalc
    {
        public Func<ICharacter, int> StaminaCalculation { get; }

        public StaminaEffect(Func<ICharacter, int> staminaCalculation)
        {
            StaminaCalculation = staminaCalculation;
        }

        public void Apply(ICombatEntity sourceEntity, IReadOnlyCollection<ICharacter> targetCharacters)
        {
            if (sourceEntity is ICharacter sourceCharacter)
            {
                int stamina = StaminaCalculation(sourceCharacter);
                foreach (ICharacter targetCharacter in targetCharacters)
                {
                    targetCharacter.Stats.ModifyStat(StatType.STA, stamina);
                }
            }
        }
    }
}