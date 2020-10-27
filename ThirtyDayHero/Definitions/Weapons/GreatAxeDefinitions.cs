using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class GreatAxeDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_GREATEAXE);

        public static readonly IWeapon HalberdItem = new Weapon(
            IdTracker.Next,
            "Halberd", "A polearm with an axe on the end.",
            WeaponType.GreatAxe,
            AttackDefinitions.Attack_STR_Fixed,
            null);

        public static readonly ItemDefinition<IWeapon> Halberd = new ItemDefinition<IWeapon>(
            "assets/weapon/greataxe/axe_23.png",
            150,
            RarityCategory.Common,
            HalberdItem);
    }
}