using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.Item;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Decorators
{
    public class ItemDefinition : IIdTracked
    {
        public string ArtName { get; }
        public IItem Item { get; }
        public uint Value { get; }
        public RarityCategory Rarity { get; }

        public uint Id { get; }

        public ItemDefinition(string artName, uint value, RarityCategory rarity, IItem item)
        {
            ArtName = artName;
            Value = value;
            Rarity = rarity;
            Item = item;
            Id = item.Id;
        }
    }
}