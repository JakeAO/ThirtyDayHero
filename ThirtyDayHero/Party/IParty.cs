using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface IParty
    {
        uint Id { get; }
        ICharacterController Controller { get; }
        IReadOnlyCollection<ICharacter> Characters { get; }
    }
}