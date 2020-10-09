using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class Character : ICharacterActor
    {
        public uint Id { get; }
        public uint Party { get; }
        public string Name { get; }
        public ICharacterClass Class { get; }
        public IStatMap Stats { get; }

        public bool CanTarget => Alive;

        public bool Alive => Stats[StatType.HP] > 0u;
        public float Initiative => Stats[StatType.DEX] / (float) Stats[StatType.LVL];

        public Character()
            : this(0, 0, string.Empty, NullClass.Instance, NullStatMap.Instance)
        {
        }

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

        public virtual IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ITargetableActor> possibleTargets)
        {
            List<IAction> actions = new List<IAction>(10);

            foreach (IAbility ability in Class.GetAllAbilities(Stats[StatType.LVL]))
            {
                actions.AddRange(ActionUtil.GetActionsForAbility(ability, this, possibleTargets));
            }

            actions.Add(new WaitAction(this));

            return actions;
        }
    }
}