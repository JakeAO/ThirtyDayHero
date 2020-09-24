namespace ThirtyDayHero
{
    public interface IPlayerCharacter : ICharacter
    {
        IEquipMap Equipment { get; }
    }
}