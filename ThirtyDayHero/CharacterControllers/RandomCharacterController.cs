using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirtyDayHero
{
    public class RandomCharacterController : ICharacterController
    {
        private static readonly Random RANDOM = new Random();

        public void SelectAction(IInitiativeActor activeEntity, IReadOnlyDictionary<uint, IAction> availableActions, Action<uint> selectAction)
        {
            int randIdx = RANDOM.Next(0, availableActions.Count);
            uint randActionId = availableActions.ElementAt(randIdx).Key;

            selectAction(randActionId);
        }
    }
}