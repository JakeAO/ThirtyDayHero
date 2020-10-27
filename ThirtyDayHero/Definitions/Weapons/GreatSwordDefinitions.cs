using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class GreatSwordDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_GREATSWORD);

        public static readonly IWeapon ClaymoreItem = new Weapon(
            IdTracker.Next,
            "Claymore", "A heavy sword wielded with two hands.",
            WeaponType.GreatSword,
            AttackDefinitions.NewAttack(100, DamageType.Normal, StatType.DEX, 0.25f, 0.75f),
            null);

        public static readonly ItemDefinition<IWeapon> Claymore = new ItemDefinition<IWeapon>(
            "assets/weapon/greatsword/sword_03.png",
            150,
            RarityCategory.Common,
            ClaymoreItem);
    }
}