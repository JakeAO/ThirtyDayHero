using System.Collections.Generic;

namespace ThirtyDayHero.Party
{
    public class Party : IParty
    {
        public uint Id { get; }
        public ICharacterController Controller { get; }
        public IReadOnlyCollection<ICharacterActor> Characters => _characters;

        private readonly List<ICharacterActor> _characters = new List<ICharacterActor>(4);

        public Party(
            uint id,
            ICharacterController controller,
            IReadOnlyCollection<ICharacterActor> characters)
        {
            Id = id;
            Controller = controller;

            _characters.AddRange(characters);
        }
    }
}