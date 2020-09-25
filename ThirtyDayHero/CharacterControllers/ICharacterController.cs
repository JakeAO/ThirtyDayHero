using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ICharacterController
    {
        void SelectAction(IInitiativeActor activeEntity, IReadOnlyDictionary<uint, IAction> availableActions, Action<uint> selectAction);
    }
}