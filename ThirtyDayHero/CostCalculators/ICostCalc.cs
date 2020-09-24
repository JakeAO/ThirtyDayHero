namespace ThirtyDayHero
{
    public interface ICostCalc
    {
        bool CanAfford(ICombatEntity entity);
        void Pay(ICombatEntity entity);
    }
}