using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ThirtyDayHero.CharacterClasses;

namespace ThirtyDayHero
{
    public class Character : ICharacterActor
    {
        public uint Id { get; set; }
        public uint Party { get; set; }
        public string Name { get; set; }
        public ICharacterClass Class { get; set; }
        public IStatMap Stats { get; set; }

        [JsonIgnore] public bool CanTarget => Alive;

        [JsonIgnore] public bool Alive => Stats[StatType.HP] > 0u;
        [JsonIgnore] public float Initiative => Stats[StatType.DEX] / (float) Stats[StatType.LVL];

        public Character()
            : this(0, 0, string.Empty, NullClass.Instance, new StatMap())
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