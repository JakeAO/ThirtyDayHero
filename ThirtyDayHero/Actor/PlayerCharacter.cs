using System.Collections.Generic;
using System.Linq;

namespace ThirtyDayHero
{
    public class PlayerCharacter : Character, IPlayerCharacterActor
    {
        public IEquipMap Equipment { get; }

        public PlayerCharacter(
            uint id,
            uint party,
            string name,
            IPlayerClass characterClass,
            IStatMap stats,
            IEquipMap equipment)
            : base(
                id,
                party,
                name,
                characterClass,
                stats)
        {
            Equipment = equipment;
        }

        public override float GetReducedDamage(float damageAmount, DamageType damageType)
        {
            float modifiedDamage = base.GetReducedDamage(damageAmount, damageType);

            return Equipment.Armor?.GetReducedDamage(modifiedDamage, damageType) ?? modifiedDamage;
        }

        public virtual IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ICharacterActor> allCharacters)
        {
            var allActions = base.GetAllActions(allCharacters).ToList();
            allActions.InsertRange(0, Equipment.GetAllActions(this, allCharacters));
            return allActions;
        }
    }
}