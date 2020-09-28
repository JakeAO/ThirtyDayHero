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

        public override IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ITargetableActor> possibleTargets)
        {
            var allActions = base.GetAllActions(possibleTargets).ToList();
            allActions.InsertRange(0, Equipment.GetAllActions(this, possibleTargets));
            return allActions;
        }
    }
}