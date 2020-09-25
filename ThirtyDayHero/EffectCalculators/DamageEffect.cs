using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class DamageEffect : IEffectCalc
    {
        public DamageType DamageType { get; }
        public Func<ICharacterActor, uint> DamageCalculation { get; }

        public DamageEffect(DamageType damageType, Func<ICharacterActor, uint> damageCalculation)
        {
            DamageType = damageType;
            DamageCalculation = damageCalculation;
        }

        public void Apply(IInitiativeActor sourceEntity, IReadOnlyCollection<ICharacterActor> targetCharacters)
        {
            if (sourceEntity is ICharacterActor sourceCharacter)
            {
                uint damage = DamageCalculation(sourceCharacter);
                foreach (ICharacterActor targetCharacter in targetCharacters)
                {
                    float modifiedDamage = targetCharacter.GetReducedDamage(damage, DamageType);
                    targetCharacter.Stats.ModifyStat(StatType.HP, (int) -Math.Round(modifiedDamage));
                }
            }
        }
    }
}