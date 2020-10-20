using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Combat;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.CharacterClasses;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class GameplayState : BlazorState
    {
        public interface IEventOption
        {
            string Text { get; }
            string Tooltip { get; }
            uint Priority { get; }
        }

        public class SingleOption : IEventOption
        {
            public string Text { get; set; } = string.Empty;
            public string Tooltip { get; set; } = string.Empty;
            public uint Priority { get; set; } = 0;

            public Action Select { get; set; } = null;
        }

        public class MultipleOption : IEventOption
        {
            public string Text { get; set; } = string.Empty;
            public string Tooltip { get; set; } = string.Empty;
            public uint Priority { get; set; } = 0;
            public string DefaultValue { get; set; } = string.Empty;

            public IReadOnlyList<(string text, string tooltip, string value)> AlternateOptions { get; set; } = null;
            public Action<string> Select { get; set; } = null;
        }

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

        private void HandleDayTimeIncrement()
        {
            if (Party.Time == TimeOfDay.Night)
            {
                Party.Time = TimeOfDay.Morning;
                Party.Day++;
            }
            else
            {
                Party.Time++;
            }

            if (_context.TryGet(out FirebaseWrapper fbWrapper))
            {
                fbWrapper.WriteData(Party.GetDataPath(fbWrapper.UserId), Party);
            }
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

            // Combat/Hunt/Etc.
            _eventOptions.Add(new MultipleOption()
            {
                Priority = 0,
                Text = "Hunt Monsters",
                Tooltip = "Spend time tracking and hunting down dangerous monster and bandits.\nMonsters tend to be stronger at Night.",
                DefaultValue = CombatDifficulty.Normal.ToString(),
                Select = OnCombatEventSelected,
                AlternateOptions = new[]
                {
                    ("Hunt Weaker Monsters", "Track monsters below your party's level.", CombatDifficulty.Easy.ToString()),
                    ("Hunt Normal Monsters", "Track monsters close to your party's level.", CombatDifficulty.Normal.ToString()),
                    ("Hunt Stronger Monsters", "Track monsters above your party's level.", CombatDifficulty.Hard.ToString())
                }
            });

            // Rest
            _eventOptions.Add(new SingleOption()
            {
                Priority = 999,
                Text = "Rest at Camp",
                Tooltip = "Resting at your camp will pass time to restore HP and STA.\nResting is most effective at Night.",
                Select = OnRestEventSelected
            });
        }

        private void OnRestEventSelected()
        {
            foreach (PlayerCharacter playerCharacter in Party.Characters)
            {
                if (playerCharacter.IsAlive())
                {
                    playerCharacter.Stats.ModifyStat(StatType.HP, (int) (playerCharacter.Stats[StatType.HP_Max] * 0.25f));
                }
            }

            _stateMachine.ChangeState<GameplayState>();
        }

        private void OnCombatEventSelected(string variantType)
        {
            if (Enum.TryParse(variantType, out CombatDifficulty combatDifficulty))
            {
                _context.Set(
                    CombatSettings.CreateFromDifficulty(
                        combatDifficulty,
                        Party));
                _stateMachine.ChangeState<CombatRunState>();
            }
        }

        private void OnCalamityEventSelected()
        {
            _context.Set(
                CombatSettings.CreateFromEnemies(
                    new[] {HackUtil.GetDefinition<EnemyDefinition>(Party.CalamityId)},
                    CombatDifficulty.Normal,
                    Party));
            _stateMachine.ChangeState<CombatRunState>();
        }
    }
}