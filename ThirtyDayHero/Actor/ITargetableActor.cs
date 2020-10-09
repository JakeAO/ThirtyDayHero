using Newtonsoft.Json;

namespace ThirtyDayHero
{
    public interface ITargetableActor : IInitiativeActor
    {
        [JsonIgnore] bool CanTarget { get; }
    }
}