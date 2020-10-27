using System;
using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Combat;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Games.ThirtyDayHero.Core.Utilities;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class PatrolState : BlazorState
    {
        private static readonly IReadOnlyCollection<(CombatDifficulty, double)> _difficultyRarityDay = new[]
        {
            (CombatDifficulty.Easy, 25d),
            (CombatDifficulty.Normal, 50d),
            (CombatDifficulty.Hard, 25d)
        };
        private static readonly IReadOnlyCollection<(CombatDifficulty, double)> _difficultyRarityNight = new[]
        {
            (CombatDifficulty.Easy, 10d),
            (CombatDifficulty.Normal, 25d),
            (CombatDifficulty.Hard, 50d)
        };

        public override Type RenderType => typeof(PatrolStatePage);

        public string EncounterText { get; private set; }
        public string DifficultyText { get; private set; }
        public bool? SneakResult { get; private set; }
        public string SneakChanceText { get; private set; }
        public string SneakChanceTooltip { get; private set; }

        private SkillCheck _sneakCheck = null;
        private CombatSettings _combatSettings = null;

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            SneakResult = null;
            
            PartyDataWrapper party = context.Get<PartyDataWrapper>();
            CombatDifficulty difficulty = RandomResultGenerator.Get(party.Time == TimeOfDay.Night
                ? _difficultyRarityNight
                : _difficultyRarityDay);
            
            EnemyGroup enemyGroup = HackUtil.GetRandomEnemyGroup();
            _combatSettings = CombatSettings.CreateFromEnemyGroup(enemyGroup, difficulty, party);

            EncounterText = enemyGroup.Description;
            switch (difficulty)
            {
                case CombatDifficulty.Easy:
                    DifficultyText = "They look pretty weak.";
                    break;
                case CombatDifficulty.Hard:
                    DifficultyText = "They look pretty tough.";
                    break;
            }

            double avgPlayerDex = party.Characters.Average(x => x.Stats[StatType.DEX]);
            double avgEnemyInt = _combatSettings.Enemies.Average(x => x.Stats[StatType.INT]);
            _sneakCheck = new SkillCheck(StatType.DEX, avgPlayerDex, StatType.INT, avgEnemyInt);

            SneakChanceText = _sneakCheck.Chance.ToString("P0");
            SneakChanceTooltip = $"Party DEX ({(uint) avgPlayerDex}) vs Enemy INT ({(uint) avgEnemyInt})";
        }

        public void EnterCombat()
        {
            IState combatRunState = new CombatRunState(_combatSettings);
            _context.Get<IStateMachine>().ChangeState(combatRunState);
        }

        public void Continue()
        {
            _context.Get<IStateMachine>().ChangeState<GameplayState>();
        }

        public void SneakAway()
        {
            SneakResult = _sneakCheck.GetResult();
        }
    }
}