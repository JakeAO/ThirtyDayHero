using System;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Util.CombatEngine.CharacterClasses;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class GameplayState : BlazorState
    {
        public override Type RenderType => typeof(GameplayStatePage);

        public PartyDataWrapper Party = null;
        public ICharacterClass Calamity = null;

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Party = context.Get<PartyDataWrapper>();
            Calamity = HackUtil.GetDefinition<ICharacterClass>(Party.CalamityId);
            
            
        }
    }
}