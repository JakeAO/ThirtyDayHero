namespace ThirtyDayHero
{
    public class StatCost : ICostCalc
    {
        public StatType Type { get; }
        public uint Amount { get; }

        public StatCost(StatType type, uint amount)
        {
            Type = type;
            Amount = amount;
        }

        public bool CanAfford(ICombatEntity entity)
        {
            return entity is ICharacter character && character.Stats.GetStat(Type) >= Amount;
        }

        public void Pay(ICombatEntity entity)
        {
            if (entity is ICharacter character)
                character.Stats.ModifyStat(Type, (int) -Amount);
        }
    }
}