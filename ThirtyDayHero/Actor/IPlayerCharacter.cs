namespace ThirtyDayHero
{
    public interface IPlayerCharacterActor : ICharacterActor
    {
        IEquipMap Equipment { get; }
    }
}