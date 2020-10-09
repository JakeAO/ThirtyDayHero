using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ICharacterClass : IIdTracked
    {
        string Name { get; }
        string Desc { get; }
        IReadOnlyDictionary<DamageType, float> IntrinsicDamageModification { get; }
        IStatMapBuilder StartingStats { get; }
        IStatMapIncrementor LevelUpStats { get; }
        IReadOnlyDictionary<uint, IReadOnlyCollection<IAbility>> AbilitiesPerLevel { get; }

        IReadOnlyCollection<IAbility> GetAllAbilities(uint level);
    }
}