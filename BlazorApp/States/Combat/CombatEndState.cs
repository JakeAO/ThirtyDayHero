using System;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Combat
{
    public class CombatEndState : BlazorState
    {
        public override Type RenderType => typeof(CombatEndStatePage);

        public CombatResults Results { get; private set; }

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Results = context.Get<CombatResults>();
            context.Clear<CombatResults>();
        }
    }
}