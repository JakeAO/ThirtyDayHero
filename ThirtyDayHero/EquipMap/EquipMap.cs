using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class EquipMap : IEquipMap
    {
        public IWeapon Weapon { get; private set; }
        public IArmor Armor { get; private set; }
        public IItem ItemA { get; private set; }
        public IItem ItemB { get; private set; }

        public EquipMap(
            IWeapon weapon = null,
            IArmor armor = null,
            IItem itemA = null,
            IItem itemB = null)
        {
            Weapon = weapon;
            Armor = armor;
            ItemA = itemA;
            ItemB = itemB;
        }

        public IReadOnlyCollection<IAction> GetAllActions(ICharacter activeChar, IReadOnlyCollection<ICharacter> allChar)
        {
            List<IAction> allActions = new List<IAction>(10);
            if (Weapon != null)
            {
                allActions.AddRange(Weapon.GetAllActions(activeChar, allChar, true));
            }

            if (Armor != null)
            {
                allActions.AddRange(Armor.GetAllActions(activeChar, allChar, true));
            }

            if (ItemA != null)
            {
                allActions.AddRange(ItemA.GetAllActions(activeChar, allChar, false));
            }

            if (ItemB != null)
            {
                allActions.AddRange(ItemB.GetAllActions(activeChar, allChar, false));
            }

            return allActions;
        }
    }
}