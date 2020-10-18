using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class SwordDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_SWORD);

        public static readonly IWeapon ShortSword = new Weapon(
            IdTracker.Next,
            "Short Sword", "A short sword common among military recruits.",
            WeaponType.Sword,
            AttackDefinitions.Attack_STR_Fixed,
            null);
    }
}