using System.Collections.Generic;

namespace ThirtyDayHero.Party
{
    public class Party : IParty
    {
        public uint Id { get; }
        public ICharacterController Controller { get; }
        public IReadOnlyCollection<ICharacter> Characters => _characters;

        private readonly List<ICharacter> _characters = new List<ICharacter>(4);

        public Party(
            uint id,
            ICharacterController controller,
            IReadOnlyCollection<ICharacter> characters)
        {
            Id = id;
            Controller = controller;

            _characters.AddRange(characters);
        }
    }
}