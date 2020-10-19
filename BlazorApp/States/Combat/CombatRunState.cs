﻿using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.GameState;
using SadPumpkin.Util.CombatEngine.Party;
using SadPumpkin.Util.CombatEngine.StateChangeEvents;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Combat
{
    public class CombatRunState : BlazorState
    {
        public override Type RenderType => typeof(CombatRunStatePage);

        public event Action NeedsToRender;
        public event Action<IStateChangeEvent> StateChangeEvent;

        public IGameState CurrentGameState { get; private set; }
        public PlayerCharacterController PlayerController { get; private set; }
        public CombatSettings CombatSettings { get; private set; }
        public IReadOnlyList<IStateChangeEvent> StateChangeRecord => _stateChangeRecord;

        private PartyDataWrapper _partyDataWrapper = null;
        private CombatManager _combatManager = null;

        private readonly List<IStateChangeEvent> _stateChangeRecord = new List<IStateChangeEvent>(100);

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            _partyDataWrapper = context.Get<PartyDataWrapper>();
            CombatSettings = context.Get<CombatSettings>();

            context.Clear<CombatSettings>();

            PlayerController = new PlayerCharacterController(_partyDataWrapper);
            PlayerController.CombatCompleteSignal.Listen(OnCombatComplete);
            PlayerController.GameStateUpdatedSignal.Listen(OnGameStateUpdated);
            PlayerController.ActiveCharacterChanged += OnActiveCharacterChanged;

            IParty playerParty = new Party(
                PlayerController.PartyId,
                PlayerController,
                _partyDataWrapper.Characters);

            IParty enemyParty = new Party(
                CombatSettings.PartyId,
                CombatSettings.AI,
                CombatSettings.Enemies);
            _combatManager = new CombatManager(
                new[] {playerParty, enemyParty},
                PlayerController.GameStateUpdatedSignal,
                PlayerController.CombatCompleteSignal);
        }

        private void OnActiveCharacterChanged()
        {
            NeedsToRender?.Invoke();
        }

        private void OnCombatComplete(uint winningPartyId)
        {
            NeedsToRender?.Invoke();

            if (_partyDataWrapper.PartyId == winningPartyId)
            {
                _context.Set(CombatResults.CreateSuccess(CombatSettings.Enemies, _partyDataWrapper));
            }
            else
            {
                _context.Set(CombatResults.CreateFailure());
            }

            _context.Get<IStateMachine>().ChangeState<CombatEndState>();
        }

        private void OnGameStateUpdated(IGameState newGameState, IReadOnlyList<IStateChangeEvent> changeEvents)
        {
            CurrentGameState = newGameState;

            foreach (IStateChangeEvent stateChangeEvent in changeEvents)
            {
                Console.WriteLine($"[Event] {stateChangeEvent.Description}");
                _stateChangeRecord.Add(stateChangeEvent);
                StateChangeEvent?.Invoke(stateChangeEvent);
            }

            NeedsToRender?.Invoke();
        }
    }
}