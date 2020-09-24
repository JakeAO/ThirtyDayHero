using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class RawDamageEffect : IEffectCalc
    {
        public Func<ICharacter, uint> DamageCalculation { get; }

        public RawDamageEffect(Func<ICharacter, uint> damageCalculation)
        {
            DamageCalculation = damageCalculation;
        }

        public void Apply(ICombatEntity sourceEntity, IReadOnlyCollection<ICharacter> targetCharacters)
        {
            if (sourceEntity is ICharacter sourceCharacter)
            {
                int damage = (int) -DamageCalculation(sourceCharacter);
                foreach (ICharacter targetCharacter in targetCharacters)
                {
                    targetCharacter.Stats.ModifyStat(StatType.HP, damage);
                }
            }
        }
    }
}