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

        public void Apply(ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> targetCharacters)
        {
            uint damage = DamageCalculation(sourceCharacter);
            foreach (ICharacter targetCharacter in targetCharacters)
            {
                uint modifiedDamage = targetCharacter.Equipment.Armor?.GetReducedDamage(damage, DamageType) ?? damage;
                targetCharacter.Stats.ModifyStat(StatType.HP, (int) -modifiedDamage);
            }
        }
    }
}