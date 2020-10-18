using System;
using System.Threading.Tasks;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class LoginState : BlazorState
    {
        public override Type RenderType => typeof(LoginStatePage);
    }
}