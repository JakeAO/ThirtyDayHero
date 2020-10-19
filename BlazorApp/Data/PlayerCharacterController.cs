using System;
using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Action;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CharacterControllers;
using SadPumpkin.Util.CombatEngine.Signals;

using Action = System.Action;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class PlayerCharacterController : ICharacterController
    {
        public uint PartyId { get; }
        public GameStateUpdated GameStateUpdatedSignal { get; }
        public CombatComplete CombatCompleteSignal { get; }
        public IReadOnlyCollection<IPlayerCharacterActor> Characters { get; }
        public IPlayerCharacterActor ActiveCharacter { get; private set; }
        public IReadOnlyDictionary<uint, IAction> AvailableActions { get; private set; }

        public event Action ActiveCharacterChanged;

        private Action<uint> _selectActionCallback = null;

        public PlayerCharacterController(PartyDataWrapper party)
        {
            PartyId = (uint)party.PartyId.GetHashCode();
            Characters = party.Characters;
            
            GameStateUpdatedSignal = new GameStateUpdated();
            CombatCompleteSignal = new CombatComplete();
        }

        public void SelectAction(IInitiativeActor activeEntity, IReadOnlyDictionary<uint, IAction> availableActions, Action<uint> selectAction)
        {
            ActiveCharacter = activeEntity as IPlayerCharacterActor;
            AvailableActions = availableActions;
            _selectActionCallback = selectAction;

            ActiveCharacterChanged?.Invoke();
        }

        public void SubmitActionResponse(uint actionId)
        {
            if (ActiveCharacter == null ||
                AvailableActions == null ||
                !AvailableActions.ContainsKey(actionId))
            {
                return;
            }

            _selectActionCallback?.Invoke(actionId);

            ActiveCharacter = null;
            AvailableActions = null;
            _selectActionCallback = null;

            ActiveCharacterChanged?.Invoke();
        }
    }
}