using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class AxeDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_AXE);

        public static readonly IWeapon Hatchet = new Weapon(
            IdTracker.Next,
            "Hatchet", "A simple woodsman's hatchet.",
            WeaponType.Axe,
            AttackDefinitions.Attack_STR_Fixed,
            null);
    }
}