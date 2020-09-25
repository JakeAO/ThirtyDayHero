using System;

namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class GreatSwordDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.GreatSword * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon Claymore = new Weapon(
            NextId,
            "Claymore", "A heavy sword wielded with two hands.",
            WeaponType.GreatSword,
            new Ability(AbilityUtil.NextId,
                "Attack", "Attack with your weapon.",
                100,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Normal,
                    source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.STR) * 1.2f))),
            null);
    }
}