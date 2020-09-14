using System;

namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class RodDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.Rod * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon FireWand = new Weapon(
            NextId,
            "Fire Wand", "A wand enchanted with the power of fire.",
            WeaponType.Rod,
            new Ability(AbilityUtil.NextId,
                "Ember", "Launch a small fireball at an enemy.",
                30,
                NoRequirements.Instance,
                new StatCost(StatType.STA, 5),
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Fire,
                    source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.MAG) * 1f))),
            null);
    }
}