using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class FistDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_FIST);

        public static readonly IWeapon LeatherGlovesItem = new Weapon(
            IdTracker.Next,
            "Leather Gloves", "Simple, reliable gloves.",
            WeaponType.Fist,
            AttackDefinitions.NewAttack(90, DamageType.Normal, StatType.STR, 0.5f, 0.5f),
            null);

        public static readonly ItemDefinition<IWeapon> LeatherGloves = new ItemDefinition<IWeapon>(
            "assets/weapon/fist/gloves_25.png",
            150,
            RarityCategory.Common,
            LeatherGlovesItem);
    }
}