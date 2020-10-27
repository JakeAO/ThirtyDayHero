using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class BowDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_BOW);

        public static readonly IWeapon ShortBowItem = new Weapon(
            IdTracker.Next,
            "Shortbow", "A simple bow with low draw weight.",
            WeaponType.Bow,
            AttackDefinitions.NewAttack(100, DamageType.Normal, StatType.DEX, 0.4f, 0.5f),
            null);

        public static readonly ItemDefinition<IWeapon> ShortBow = new ItemDefinition<IWeapon>(
            "assets/weapon/bow/bow_02.png",
            150,
            RarityCategory.Common,
            ShortBowItem);
    }
}