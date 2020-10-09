using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ThirtyDayHero
{
    public class Weapon : IWeapon
    {
        public uint Id { get; }
        public string Name { get; }
        public string Desc { get; }
        public ItemType ItemType { get; }
        public WeaponType WeaponType { get; }

        private readonly IAbility _attackAbility;
        private readonly IReadOnlyCollection<IAbility> _addedAbilities;

        public Weapon()
            : this(0,
                string.Empty, string.Empty,
                WeaponType.Invalid,
                null,
                null)
        {
        }

        public Weapon(
            uint id,
            string name, string desc,
            WeaponType weaponType,
            IAbility attackAbility,
            IReadOnlyCollection<IAbility> addedAbilities)
        {
            Id = id;
            Name = name;
            Desc = desc;
            ItemType = ItemType.Weapon;
            WeaponType = weaponType;

            _attackAbility = attackAbility;
            _addedAbilities = addedAbilities != null
                ? new List<IAbility>(addedAbilities)
                : new List<IAbility>();
        }

        public IReadOnlyCollection<IAction> GetAllActions(ICharacterActor sourceCharacter, IReadOnlyCollection<ITargetableActor> possibleTargets, bool isEquipped)
        {
            List<IAction> actions = new List<IAction>(10);

            if (_attackAbility != null && isEquipped)
            {
                actions.AddRange(ActionUtil.GetActionsForAbility(_attackAbility, sourceCharacter, possibleTargets));
            }

            if (_addedAbilities != null)
            {
                foreach (IAbility ability in _addedAbilities)
                {
                    actions.AddRange(ActionUtil.GetActionsForAbility(ability, sourceCharacter, possibleTargets));
                }
            }

            return actions;
        }
    }
}