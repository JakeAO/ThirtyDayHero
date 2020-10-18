using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class StaffDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_STAFF);

        public static readonly IWeapon Quarterstaff = new Weapon(
            IdTracker.Next,
            "Quarterstaff", "A simple wooden pole.",
            WeaponType.Staff,
            AttackDefinitions.Attack_STR_Fixed,
            null);
    }
}