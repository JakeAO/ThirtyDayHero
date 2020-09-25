using System;

namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class SpearDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.Spear * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon Spear = new Weapon(
            NextId,
            "Spear", "A simple militia spear.",
            WeaponType.Spear,
            new Ability(AbilityUtil.NextId,
                "Attack", "Attack with your weapon.",
                100,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Normal,
                    source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.STR) * 1.1f))),
            null);
    }
}