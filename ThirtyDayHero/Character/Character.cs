using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class Character : ICharacter
    {
        public uint Id { get; }
        public uint Party { get; }
        public string Name { get; }
        public ICharacterClass Class { get; }
        public IStatMap Stats { get; }
        public IEquipMap Equipment { get; }

        public Character(
            uint id,
            uint party,
            string name,
            ICharacterClass characterClass,
            IStatMap stats,
            IEquipMap equipment)
        {
            Id = id;
            Party = party;
            Name = name;
            Class = characterClass;
            Stats = stats;
            Equipment = equipment;
        }

        public IReadOnlyCollection<IAction> GetAllActions(IReadOnlyCollection<ICharacter> allCharacters)
        {
            List<IAction> actions = new List<IAction>(10);

            actions.AddRange(Equipment.GetAllActions(this, allCharacters));

            foreach (IAbility ability in Class.GetAllAbilities(Stats.GetStat(StatType.LVL)))
            {
                actions.AddRange(ActionUtil.GetActionsForAbility(ability, this, allCharacters));
            }

            actions.Add(new WaitAction(this));
            
            return actions;
        }
    }
}