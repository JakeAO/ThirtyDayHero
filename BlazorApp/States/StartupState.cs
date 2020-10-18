using System;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class StartupState : BlazorState
    {
        public override Type RenderType => typeof(StartupStatePage);
    }
}