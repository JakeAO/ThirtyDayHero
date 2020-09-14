using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirtyDayHero
{
    public class RandomCharacterController : ICharacterController
    {
        private static readonly Random RANDOM = new Random();

        public void SelectAction(ICharacter activeCharacter, IReadOnlyDictionary<uint, IAction> availableActions, Action<uint> onActionSelected)
        {
            int randIdx = RANDOM.Next(0, availableActions.Count);
            uint randActionId = availableActions.ElementAt(randIdx).Key;

            onActionSelected(randActionId);
        }
    }
}