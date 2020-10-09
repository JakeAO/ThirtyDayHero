using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class EquipMap : IEquipMap
    {
        public IWeapon Weapon { get; set; }
        public IArmor Armor { get; set; }
        public IItem ItemA { get; set; }
        public IItem ItemB { get; set; }

        public EquipMap()
            : this(null, null, null, null)
        {
        }

        public EquipMap(
            IWeapon weapon,
            IArmor armor,
            IItem itemA,
            IItem itemB)
        {
            Weapon = weapon;
            Armor = armor;
            ItemA = itemA;
            ItemB = itemB;
        }

        public IReadOnlyCollection<IAction> GetAllActions(ICharacterActor activeChar, IReadOnlyCollection<ITargetableActor> possibleTargets)
        {
            List<IAction> allActions = new List<IAction>(10);
            if (Weapon != null)
            {
                allActions.AddRange(Weapon.GetAllActions(activeChar, possibleTargets, true));
            }

            if (Armor != null)
            {
                allActions.AddRange(Armor.GetAllActions(activeChar, possibleTargets, true));
            }

            if (ItemA != null)
            {
                allActions.AddRange(ItemA.GetAllActions(activeChar, possibleTargets, false));
            }

            if (ItemB != null)
            {
                allActions.AddRange(ItemB.GetAllActions(activeChar, possibleTargets, false));
            }

            return allActions;
        }
    }
}