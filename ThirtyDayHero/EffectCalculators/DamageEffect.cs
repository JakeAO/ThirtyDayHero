using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class DamageEffect : IEffectCalc
    {
        public DamageType DamageType { get; }
        public Func<ICharacter, uint> DamageCalculation { get; }

        public DamageEffect(DamageType damageType, Func<ICharacter, uint> damageCalculation)
        {
            DamageType = damageType;
            DamageCalculation = damageCalculation;
        }

        public void Apply(ICombatEntity sourceEntity, IReadOnlyCollection<ICharacter> targetCharacters)
        {
            if (sourceEntity is ICharacter sourceCharacter)
            {
                uint damage = DamageCalculation(sourceCharacter);
                foreach (ICharacter targetCharacter in targetCharacters)
                {
                    float modifiedDamage = targetCharacter.GetReducedDamage(damage, DamageType);
                    targetCharacter.Stats.ModifyStat(StatType.HP, (int) -Math.Round(modifiedDamage));
                }
            }
        }
    }
}