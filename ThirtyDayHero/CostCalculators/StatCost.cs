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

        public bool CanAfford(IInitiativeActor entity)
        {
            return entity is ICharacterActor character && character.Stats.GetStat(Type) >= Amount;
        }

        public void Pay(IInitiativeActor entity)
        {
            if (entity is ICharacterActor character)
                character.Stats.ModifyStat(Type, (int) -Amount);
        }
    }
}