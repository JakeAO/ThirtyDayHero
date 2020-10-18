﻿using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class FistDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_FIST);

        public static readonly IWeapon LeatherGloves = new Weapon(
            IdTracker.Next,
            "Leather Gloves", "Simple, reliable gloves.",
            WeaponType.Fist,
            AttackDefinitions.Attack_STR_Fixed,
            null);
    }
}