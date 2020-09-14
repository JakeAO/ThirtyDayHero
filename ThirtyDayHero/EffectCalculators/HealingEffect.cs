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

        public void Apply(ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> targetCharacters)
        {
            int healing = (int) HealingCalculation(sourceCharacter);
            foreach (ICharacter targetCharacter in targetCharacters)
            {
                targetCharacter.Stats.ModifyStat(StatType.HP, healing);
            }
        }
    }
}