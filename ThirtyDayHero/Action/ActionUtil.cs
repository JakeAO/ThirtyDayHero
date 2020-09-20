﻿using System.Collections.Generic;

namespace ThirtyDayHero
{
    public static class ActionUtil
    {
        private static uint _nextId = 90000;
        public static uint NextId => ++_nextId;

        public static IEnumerable<IAction> GetActionsForAbility(IAbility ability, ICharacter sourceCharacter, IReadOnlyCollection<ICharacter> allCharacters)
        {
            if (ability.Requirements.MeetsRequirement(sourceCharacter) &&
                ability.Cost.CanAfford(sourceCharacter) &&
                ability.Target.GetTargetOptions(sourceCharacter, allCharacters) is var targetOptions &&
                targetOptions.Count > 0)
            {
                foreach (IReadOnlyCollection<ICharacter> targets in targetOptions)
                {
                    yield return new Action(NextId, true, ability, sourceCharacter, targets);
                }
            }
            else
            {
                yield return new Action(NextId, false, ability, sourceCharacter, null);
            }
        }
    }
}