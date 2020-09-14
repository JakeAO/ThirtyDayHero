using System;

namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class BowDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.Bow * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon ShortBow = new Weapon(
            NextId,
            "Shortbow", "A simple bow with low draw weight.",
            WeaponType.Bow,
            new Ability(AbilityUtil.NextId,
                "Attack", "Attack with your weapon.",
                30,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Normal,
                    source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.DEX) * 1f))),
            null);
    }
}