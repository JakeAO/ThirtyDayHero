using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Games.ThirtyDayHero.Core.Utilities;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class RestState : BlazorState
    {
        public enum RestQuality
        {
            Bad,
            Good,
            Great
        }

        private static readonly Random RANDOM = new Random();

        private static readonly IReadOnlyCollection<(RestQuality, double)> _qualityRarityDay = new[]
        {
            (RestQuality.Bad, 30d),
            (RestQuality.Good, 30d),
            (RestQuality.Great, 5d)
        };

        private static readonly IReadOnlyCollection<(RestQuality, double)> _qualityRarityNight = new[]
        {
            (RestQuality.Bad, 5d),
            (RestQuality.Good, 30d),
            (RestQuality.Great, 30d)
        };

        private static readonly IReadOnlyDictionary<RestQuality, (float baseVal, float randVal)> _restAmountByQuality = new Dictionary<RestQuality, (float baseVal, float randVal)>()
        {
            {RestQuality.Bad, (0.05f, 0.05f)},
            {RestQuality.Good, (0.10f, 0.10f)},
            {RestQuality.Great, (0.15f, 0.15f)}
        };

        public override Type RenderType => typeof(RestStatePage);

        public PartyDataWrapper Party { get; private set; }
        public RestQuality RestInfo { get; private set; }
        public string HpRestored { get; private set; }
        public string StaRestored { get; private set; }

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Party = _context.Get<PartyDataWrapper>();

            RestInfo = RandomResultGenerator.Get(Party.Time == TimeOfDay.Night
                ? _qualityRarityNight
                : _qualityRarityDay);
            (float baseVal, float randVal) = _restAmountByQuality[RestInfo];

            double hpRestorePerc = baseVal + randVal * RANDOM.NextDouble();
            HpRestored = hpRestorePerc.ToString("P0");

            double staRestorePerc = baseVal + randVal * RANDOM.NextDouble();
            StaRestored = staRestorePerc.ToString("P0");

            foreach (PlayerCharacter playerCharacter in Party.Characters)
            {
                uint maxHp = playerCharacter.Stats[StatType.HP_Max];
                int hpRestored = (int) Math.Round(maxHp * hpRestorePerc);

                uint maxSta = playerCharacter.Stats[StatType.STA_Max];
                int staRestored = (int) Math.Round(maxSta * staRestorePerc);

                playerCharacter.Stats.ModifyStat(StatType.HP, hpRestored);
                playerCharacter.Stats.ModifyStat(StatType.STA, staRestored);
            }
        }

        public void Continue()
        {
            _context.Get<IStateMachine>().ChangeState<GameplayState>();
        }
    }
}