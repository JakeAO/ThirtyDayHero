using System;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public abstract class BlazorState : IState
    {
        protected Context _context = null;

        public virtual Type RenderType => typeof(NotImplementedExceptionPage);
        
        public virtual void PerformSetup(Context context, IState previousState)
        {
            _context = context;
            Console.WriteLine($"[State] Transitioning from {previousState?.GetType().Name ?? "NULL"} to {GetType().Name}");
        }

        public virtual void PerformContent(Context context)
        {
            _context = context;
        }

        public virtual void PerformTeardown(Context context, IState nextState)
        {
            _context = context;
        }
    }
}