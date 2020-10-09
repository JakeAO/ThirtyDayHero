using System;
using System.Collections.Generic;
using System.Security;
using Newtonsoft.Json;

namespace ThirtyDayHero
{
    public class Armor : IArmor
    {
        public uint Id { get; }
        public string Name { get; }
        public string Desc { get; }
        public ItemType ItemType { get; }
        public ArmorType ArmorType { get; }

        private readonly IReadOnlyDictionary<DamageType, float> _damageModifiers;
        private readonly IReadOnlyCollection<IAbility> _addedAbilities;

        public Armor()
            : this(0,
                string.Empty, String.Empty,
                ArmorType.Invalid,
                null, 
                null)
        {
        }

        public Armor(
            uint id,
            string name, string desc,
            ArmorType armorType,
            IReadOnlyDictionary<DamageType, float> damageModifiers,
            IReadOnlyCollection<IAbility> addedAbilities)
        {
            Id = id;
            Name = name;
            Desc = desc;
            ItemType = ItemType.Armor;
            ArmorType = armorType;

            _damageModifiers = damageModifiers != null
                ? new Dictionary<DamageType, float>(damageModifiers)
                : new Dictionary<DamageType, float>();
            _addedAbilities = addedAbilities != null
                ? new List<IAbility>(addedAbilities)
                : new List<IAbility>();
        }

        public IReadOnlyCollection<IAction> GetAllActions(ICharacterActor sourceCharacter, IReadOnlyCollection<ITargetableActor> possibleTargets, bool isEquipped)
        {
            List<IAction> actions = new List<IAction>(10);

            if (_addedAbilities != null)
            {
                foreach (IAbility ability in _addedAbilities)
                {
                    actions.AddRange(ActionUtil.GetActionsForAbility(ability, sourceCharacter, possibleTargets));
                }
            }

            return actions;
        }

        public float GetReducedDamage(float damageAmount, DamageType damageType)
        {
            if (_damageModifiers != null && _damageModifiers.Count > 0)
            {
                float damage = damageAmount;
                if (_damageModifiers.TryGetValue(damageType, out float modifier))
                {
                    damage *= modifier;
                }
                else
                {
                    foreach (int enumValue in Enum.GetValues(typeof(DamageType)))
                    {
                        DamageType enumType = (DamageType) enumValue;
                        if ((enumValue & (int) damageType) == enumValue &&
                            _damageModifiers.TryGetValue(enumType, out modifier))
                        {
                            damage *= modifier;
                        }
                    }
                }

                return damage;
            }
            else
            {
                return damageAmount;
            }
        }
    }
}