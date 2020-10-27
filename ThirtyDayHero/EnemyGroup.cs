using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine;

namespace SadPumpkin.Games.ThirtyDayHero.Core
{
    public class EnemyGroup : IIdTracked
    {
        public uint Id { get; }
        public RarityCategory Rarity { get; }
        public string Description { get; }
        public IReadOnlyCollection<EnemyDefinition> EnemyTypes { get; }
        public IReadOnlyDictionary<EnemyDefinition, double> EnemyTypesByRarity { get; }

        public EnemyGroup(
            uint id,
            RarityCategory rarity,
            string description,
            IReadOnlyCollection<EnemyDefinition> enemyTypes)
        {
            Id = id;
            Rarity = rarity;
            Description = description;
            EnemyTypes = enemyTypes;
            EnemyTypesByRarity = EnemyTypes.ToDictionary(
                x => x,
                x => (double) x.Rarity);
        }
    }
}