using System;

namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class GreatAxeDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.GreatAxe * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon Halberd = new Weapon(
            NextId,
            "Halberd", "A polearm with an axe on the end.",
            WeaponType.GreatAxe,
            new Ability(AbilityUtil.NextId,
                "Attack", "Attack with your weapon.",
                50,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Normal,
                    source => (uint) Math.Ceiling(source.Stats.GetStat(StatType.STR) * 1.2f))),
            null);
    }
}