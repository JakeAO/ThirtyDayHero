using System;

namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class AxeDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.Axe * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon Hatchet = new Weapon(
            NextId,
            "Hatchet", "A simple woodsman's hatchet.",
            WeaponType.Axe,
            new Ability(AbilityUtil.NextId,
                "Attack", "Attack with your weapon.",
                25,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Normal,
                    source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.STR) * 0.95f))),
            null);
    }
}