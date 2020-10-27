using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.Item;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Decorators
{
    public abstract class ItemDefinition : IIdTracked
    {
        public abstract string ArtName { get; }
        public abstract IItem Item { get; }
        public abstract uint Value { get; }
        public abstract RarityCategory Rarity { get; }

        public abstract uint Id { get; }
    }

    public class ItemDefinition<T> : ItemDefinition where T : IItem
    {
        public override string ArtName { get; }
        public override IItem Item { get; }
        public override uint Value { get; }
        public override RarityCategory Rarity { get; }

        public override uint Id { get; }

        public T TypedItem { get; }

        public ItemDefinition(string artName, uint value, RarityCategory rarity, T item)
        {
            ArtName = artName;
            Value = value;
            Rarity = rarity;
            Item = TypedItem = item;
            Id = item.Id;
        }
    }
}