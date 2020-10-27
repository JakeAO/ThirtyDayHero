using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class CalamityDefeatedState : BlazorState
    {
        public override Type RenderType => typeof(CalamityDefeatedStatePage);

        public Character Calamity { get; private set; }
        public IReadOnlyList<PlayerCharacter> SurvivingCharacters { get; set; }
        public IReadOnlyList<PlayerCharacter> FallenCharacters { get; set; }

        public override void PerformSetup(Context context, IState previousState)
        {
            PartyDataWrapper party = context.Get<PartyDataWrapper>();
            FirebaseWrapper fbWrapper = context.Get<FirebaseWrapper>();
            PlayerDataWrapper playerData = context.Get<PlayerDataWrapper>();

            playerData.SetActiveParty(0u);

            fbWrapper.WriteData(playerData.GetDataPath(fbWrapper.UserId), playerData);

            _context.Clear<PartyDataWrapper>();

            Calamity = party.Calamity;
            SurvivingCharacters = party.Characters.FindAll(x => x.IsAlive());
            FallenCharacters = party.Characters.FindAll(x => !x.IsAlive());
        }

        public void Continue()
        {
            IStateMachine stateMachine = _context.Get<IStateMachine>();
            stateMachine.ChangeState<CreatePartyState>();
        }
    }
}