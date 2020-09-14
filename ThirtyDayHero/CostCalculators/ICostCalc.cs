namespace ThirtyDayHero
{
    public interface ICostCalc
    {
        bool CanAfford(ICharacter character);
        void Pay(ICharacter character);
    }
}