using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IEquipMap
    {
        IWeapon Weapon { get; }
        IArmor Armor { get; }
        IItem ItemA { get; }
        IItem ItemB { get; }

        IReadOnlyCollection<IAction> GetAllActions(ICharacter activeChar, IReadOnlyCollection<ICharacter> allChar);
    }
}