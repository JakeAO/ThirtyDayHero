using System;
using System.Collections.Generic;

namespace ThirtyDayHero
{
    public interface ICharacterController
    {
        void SelectAction(ICombatEntity activeEntity, IReadOnlyDictionary<uint, IAction> availableActions, Action<uint> onActionSelected);
    }
}