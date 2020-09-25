namespace ThirtyDayHero.Item.Weapons.Definitions
{
    public static class SwordDefinitions
    {
        private static uint _nextId = 60000 + (int) WeaponType.Sword * 1000;
        public static uint NextId => ++_nextId;

        public static readonly IWeapon ShortSword = new Weapon(
            NextId,
            "Short Sword", "A short sword common among military recruits.",
            WeaponType.Sword,
            new Ability(AbilityUtil.NextId,
                "Attack", "Attack with your weapon.",
                100,
                NoRequirements.Instance,
                NoCost.Instance,
                SingleEnemyTargetCalculator.Instance,
                new DamageEffect(
                    DamageType.Normal,
                    source => source.Stats.GetStat(StatType.STR))),
            null);
    }
}