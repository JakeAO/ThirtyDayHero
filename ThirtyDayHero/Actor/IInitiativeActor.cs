using System.Collections.Generic;
using Newtonsoft.Json;

namespace ThirtyDayHero
{
    public interface IInitiativeActor
    {
        uint Id { get; }
        uint Party { get; }
        string Name { get; }

        [JsonIgnore] float Initiative { get; }
        [JsonIgnore] bool Alive { get; }
        
        IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ITargetableActor> possibleTargets);
    }
}