using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ICharacterController
    {
        void SelectAction(ICharacter activeCharacter, IReadOnlyDictionary<uint, IAction> availableActions, Action<uint> onActionSelected);
    }
}