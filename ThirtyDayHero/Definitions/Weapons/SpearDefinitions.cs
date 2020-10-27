using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class SpearDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_SPEAR);

        public static readonly IWeapon SpearItem = new Weapon(
            IdTracker.Next,
            "Spear", "A simple militia spear.",
            WeaponType.Spear,
            AttackDefinitions.NewAttack(90, DamageType.Normal, StatType.STR, 0.3f, 0.5f),
            null);

        public static readonly ItemDefinition<IWeapon> Spear = new ItemDefinition<IWeapon>(
            "assets/weapon/spear/spear_10.png",
            150,
            RarityCategory.Common,
            SpearItem);
    }
}