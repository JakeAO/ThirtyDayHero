using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class HealingEffect : IEffectCalc
    {
        public Func<ICharacter, uint> HealingCalculation { get; }

        public HealingEffect(Func<ICharacter, uint> healingCalculation)
        {
            HealingCalculation = healingCalculation;
        }

        public void Apply(ICombatEntity sourceEntity, IReadOnlyCollection<ICharacter> targetCharacters)
        {
            if (sourceEntity is ICharacter sourceCharacter)
            {
                int healing = (int) HealingCalculation(sourceCharacter);
                foreach (ICharacter targetCharacter in targetCharacters)
                {
                    targetCharacter.Stats.ModifyStat(StatType.HP, healing);
                }
            }
        }
    }
}