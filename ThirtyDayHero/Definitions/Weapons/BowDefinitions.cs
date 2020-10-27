using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class BowDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_BOW);

        public static readonly IWeapon ShortBowItem = new Weapon(
            IdTracker.Next,
            "Shortbow", "A simple bow with low draw weight.",
            WeaponType.Bow,
            AttackDefinitions.Attack_DEX_Fixed,
            null);

        public static readonly ItemDefinition<IWeapon> ShortBow = new ItemDefinition<IWeapon>(
            "assets/weapon/bow/bow_02.png",
            150,
            RarityCategory.Common,
            ShortBowItem);
    }
}