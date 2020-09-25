using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class HealingEffect : IEffectCalc
    {
        public Func<ICharacterActor, uint> HealingCalculation { get; }

        public HealingEffect(Func<ICharacterActor, uint> healingCalculation)
        {
            HealingCalculation = healingCalculation;
        }

        public void Apply(IInitiativeActor sourceEntity, IReadOnlyCollection<ICharacterActor> targetCharacters)
        {
            if (sourceEntity is ICharacterActor sourceCharacter)
            {
                int healing = (int) HealingCalculation(sourceCharacter);
                foreach (ICharacterActor targetCharacter in targetCharacters)
                {
                    targetCharacter.Stats.ModifyStat(StatType.HP, healing);
                }
            }
        }
    }
}