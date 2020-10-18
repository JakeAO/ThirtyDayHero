using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class SpearDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_SPEAR);

        public static readonly IWeapon Spear = new Weapon(
            IdTracker.Next,
            "Spear", "A simple militia spear.",
            WeaponType.Spear,
            AttackDefinitions.Attack_STR_Fixed,
            null);
    }
}