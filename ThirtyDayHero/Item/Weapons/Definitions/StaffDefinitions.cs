using System;

namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class StaffDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.Staff * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon Quarterstaff = new Weapon(
            NextId,
            "Quarterstaff", "A simple wooden pole.",
            WeaponType.Staff,
            new Ability(AbilityUtil.NextId,
                "Attack", "Attack with your weapon.",
                100,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Normal,
                    source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.DEX) * 0.75f))),
            null);
    }
}