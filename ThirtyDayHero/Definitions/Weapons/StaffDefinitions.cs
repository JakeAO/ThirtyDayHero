using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class StaffDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_STAFF);

        public static readonly IWeapon ArcaneStaffItem = new Weapon(
            IdTracker.Next,
            "Arcane Staff", "A simple wooden staff that converts magical power into force.",
            WeaponType.Staff,
            AttackDefinitions.NewAttack(100, DamageType.Normal, StatType.MAG, 0.2f, 0.4f),
            null);

        public static readonly ItemDefinition<IWeapon> ArcaneStaff = new ItemDefinition<IWeapon>(
            "assets/weapon/staff/staff_19.png",
            150,
            RarityCategory.Common,
            ArcaneStaffItem);
    }
}