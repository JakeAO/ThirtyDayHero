using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Combat;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Town;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

using Action = SadPumpkin.Util.CombatEngine.Action.Action;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class GameplayState : BlazorState
    {
        public override Type RenderType => typeof(GameplayStatePage);

        public PartyDataWrapper Party = null;
        public IReadOnlyList<IEventOption> EventOptions => _eventOptions;

        private readonly List<IEventOption> _eventOptions = new List<IEventOption>(10);
        private IStateMachine _stateMachine = null;

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Party = context.Get<PartyDataWrapper>();
            _stateMachine = context.Get<IStateMachine>();

            // Check for Time/Day Increment
            if (!(previousState is StartupState))
            {
                HandleDayTimeIncrement();
            }

            // Determine Event Options
            DetermineEventOptions();
        }

        public void SaveParty()
        {
            if (_context.TryGet(out FirebaseWrapper fbWrapper))
            {
                fbWrapper.WriteData(Party.GetDataPath(fbWrapper.UserId), Party);
            }
        }
        
        private void HandleDayTimeIncrement()
        {
            Party.IncrementTime();

            SaveParty();
        }

        private void DetermineEventOptions()
        {
            if (Party.Day == 30)
            {
                _eventOptions.Add(new SingleOption()
                {
                    Priority = 0,
                    Text = "Face the Calamity",
                    Tooltip = "The calamity has come, now is the time to stand and face it.",
                    Select = OnCalamityEventSelected
                });
                return;
            }

            // Patrol
            _eventOptions.Add(new SingleOption()
            {
                Priority = 0,
                Text = "Patrol Nearby",
                Tooltip = "Spend time tracking and hunting down dangerous monster and bandits.\n[Enemies tend to be stronger at Night.]",
                Select = OnPatrolEventSelected
            });

            // Town
            _eventOptions.Add(new SingleOption()
            {
                Priority = 1,
                Text = "Visit Town",
                Tooltip = "Travel to the nearby town.",
                Select = OnTownEventSelected
            });
            
            // Rest
            _eventOptions.Add(new SingleOption()
            {
                Priority = 999,
                Text = "Rest at Camp",
                Tooltip = "Resting at your camp will pass time to restore HP and STA.\n[Resting is most effective at Night.]",
                Select = OnRestEventSelected
            });
        }

        private void OnRestEventSelected()
        {
            _stateMachine.ChangeState<RestState>();
        }

        private void OnPatrolEventSelected()
        {
            _stateMachine.ChangeState<PatrolState>();
        }

        private void OnTownEventSelected()
        {
            _stateMachine.ChangeState<TownHubState>();
        }
        
        private void OnCalamityEventSelected()
        {
            IState combatRunState = new CombatRunState(CombatSettings.CreateFromEnemies(new[] {Party.Calamity}));
            _stateMachine.ChangeState(combatRunState);
        }
    }
}