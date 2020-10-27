using System;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Town
{
    public class TownDojoState : BlazorState
    {
        private static readonly Random RANDOM = new Random();

        public override Type RenderType => typeof(TownDojoStatePage);

        public uint Cost { get; }
        public PartyDataWrapper Party { get; private set; }
        public uint? ExpGained { get; private set; }

        public TownDojoState(uint dojoCost)
        {
            Cost = dojoCost;
        }

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Party = context.Get<PartyDataWrapper>();
        }

        public bool CanTrainAtDojo()
        {
            return Party.Gold >= Cost;
        }

        public void TrainAtDojo()
        {
            ExpGained = (uint) Math.Round(30f + RANDOM.NextDouble() * 70f);
            Party.Gold -= Cost;
            foreach (PlayerCharacter playerCharacter in Party.Characters)
            {
                playerCharacter.Stats.ModifyStat(StatType.EXP, (int) ExpGained.Value);
            }
        }

        public void LeaveDojo()
        {
            _context.Get<IStateMachine>().ChangeState<TownHubState>();
        }
    }
}