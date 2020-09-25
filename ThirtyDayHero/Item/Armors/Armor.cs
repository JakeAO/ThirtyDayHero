using System;
using System.Collections.Generic;

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

            _damageModifiers = damageModifiers;
            _addedAbilities = addedAbilities;
        }

        public IReadOnlyCollection<IAction> GetAllActions(ICharacterActor sourceCharacter, IReadOnlyCollection<ICharacterActor> allCharacters, bool isEquipped)
        {
            List<IAction> actions = new List<IAction>(10);

            if (_addedAbilities != null)
            {
                foreach (IAbility ability in _addedAbilities)
                {
                    actions.AddRange(ActionUtil.GetActionsForAbility(ability, sourceCharacter, allCharacters));
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