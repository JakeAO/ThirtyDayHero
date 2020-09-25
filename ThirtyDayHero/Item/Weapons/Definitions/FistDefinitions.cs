using System;

namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class FistDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.Fist * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon LeatherGloves = new Weapon(
            NextId,
            "Leather Gloves", "Simple, reliable gloves.",
            WeaponType.Fist,
            new Ability(AbilityUtil.NextId,
                "Attack", "Pummel the enemy.",
                100,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Normal,
                    source => (uint) Math.Ceiling((source.Stats.GetStat(StatType.STR) + source.Stats.GetStat(StatType.DEX)) * 0.5f))),
            null);
    }
}