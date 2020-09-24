using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class Character : ICharacter
    {
        public uint Id { get; }
        public uint Party { get; }
        public string Name { get; }
        public ICharacterClass Class { get; }
        public IStatMap Stats { get; }

        public bool Alive => Stats.GetStat(StatType.HP) > 0u;
        public float Initiative => 1f + 19f / Stats.GetStat(StatType.DEX) / Stats.GetStat(StatType.LVL);

        public Character(
            uint id,
            uint party,
            string name,
            ICharacterClass characterClass,
            IStatMap stats)
        {
            Id = id;
            Party = party;
            Name = name;
            Class = characterClass;
            Stats = stats;
        }

        public virtual float GetReducedDamage(float damageAmount, DamageType damageType)
        {
            if (Class.IntrinsicDamageModification != null && Class.IntrinsicDamageModification.Count > 0)
            {
                float damage = damageAmount;
                if (Class.IntrinsicDamageModification.TryGetValue(damageType, out float modifier))
                {
                    damage *= modifier;
                }
                else
                {
                    foreach (int enumValue in Enum.GetValues(typeof(DamageType)))
                    {
                        DamageType enumType = (DamageType) enumValue;
                        if ((enumValue & (int) damageType) == enumValue &&
                            Class.IntrinsicDamageModification.TryGetValue(enumType, out modifier))
                        {
                            damage *= modifier;
                        }
                    }
                }

                return damage;
            }

            return damageAmount;
        }

        public virtual IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ICharacter> allCharacters)
        {
            List<IAction> actions = new List<IAction>(10);

            foreach (IAbility ability in Class.GetAllAbilities(Stats.GetStat(StatType.LVL)))
            {
                actions.AddRange(ActionUtil.GetActionsForAbility(ability, this, allCharacters));
            }

            actions.Add(new WaitAction(this));

            return actions;
        }
    }
}