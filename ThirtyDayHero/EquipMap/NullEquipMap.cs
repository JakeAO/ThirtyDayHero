using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class NullEquipMap : IEquipMap
    {
        public static NullEquipMap Instance = new NullEquipMap();

        private NullEquipMap()
        {
        }
        
        public IWeapon Weapon => null;
        public IArmor Armor => null;
        public IItem ItemA => null;
        public IItem ItemB => null;
        public IReadOnlyCollection<IAction> GetAllActions(ICharacterActor activeChar, IReadOnlyCollection<ITargetableActor> possibleTargets) => new IAction[0];
    }
}