using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class SwordDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_SWORD);

        public static readonly IWeapon ShortSwordItem = new Weapon(
            IdTracker.Next,
            "Short Sword", "A short sword common among military recruits.",
            WeaponType.Sword,
            AttackDefinitions.NewAttack(100, DamageType.Normal, StatType.STR, 0.4f, 0.6f),
            null);

        public static readonly ItemDefinition<IWeapon> ShortSword = new ItemDefinition<IWeapon>(
            "assets/weapon/sword/sword_01.png",
            150,
            RarityCategory.Common,
            ShortSwordItem);
    }
}