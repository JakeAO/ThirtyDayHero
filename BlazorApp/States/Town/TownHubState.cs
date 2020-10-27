using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Town
{
    public class TownHubState : BlazorState
    {
        public override Type RenderType => typeof(TownHubStatePage);

        public TownData Data { get; private set; }
        public PartyDataWrapper Party { get; private set; }
        public IReadOnlyList<IEventOption> EventOptions => _eventOptions;
        public string PromptMessageText { get; private set; }

        private FirebaseWrapper _fbWrapper = null;
        private IStateMachine _stateMachine = null;
        private readonly List<IEventOption> _eventOptions = new List<IEventOption>(5);

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Party = context.Get<PartyDataWrapper>();

            _fbWrapper = context.Get<FirebaseWrapper>();
            _stateMachine = context.Get<IStateMachine>();

            if (context.TryGet(out TownData savedData) && savedData != null)
            {
                Data = savedData;
            }
            else
            {
                Data = new TownData();
                context.Set(Data);
            }

            DeterminePrompt();
            DetermineEventOptions();

            _fbWrapper.WriteData(Party.GetDataPath(_fbWrapper.UserId), Party);
        }

        private void DeterminePrompt()
        {
            switch (Party.Time)
            {
                case TimeOfDay.Morning:
                    PromptMessageText = "The streets are bustling with activity as people go about their days.";
                    break;
                case TimeOfDay.Evening:
                    PromptMessageText = "";
                    break;
                case TimeOfDay.Night:
                    PromptMessageText = "The streets of the town are silent.";
                    break;
            }
        }

        private void DetermineEventOptions()
        {
            if (Data.HasInn)
            {
                _eventOptions.Add(new SingleOption()
                {
                    Priority = 0,
                    Text = "Inn",
                    Tooltip = "The inn allows you to get a full rest.",
                    Select = GoToInn
                });
            }

            if (Data.HasDojo)
            {
                _eventOptions.Add(new SingleOption()
                {
                    Priority = 1,
                    Text = "Dojo",
                    Tooltip = "The dojo allows you to train without the threat of harm. Closed at night.",
                    Select = GoToDojo,
                    Disabled = Party.Time == TimeOfDay.Night
                });
            }

            if (Data.HasItemShop)
            {
                _eventOptions.Add(new SingleOption()
                {
                    Priority = 2,
                    Text = "General Store",
                    Tooltip = "The general store stocks trinkets and consumables for purchase. Closed at night.",
                    Select = GoToItemShop,
                    Disabled = Party.Time == TimeOfDay.Night
                });
            }

            if (Data.HasWeaponShop)
            {
                _eventOptions.Add(new SingleOption()
                {
                    Priority = 3,
                    Text = "Weapon Shop",
                    Tooltip = "The weapon shop stocks different types of weapons for purchase. Closed at night.",
                    Select = GoToWeaponShop,
                    Disabled = Party.Time == TimeOfDay.Night
                });
            }

            if (Data.HasArmorShop)
            {
                _eventOptions.Add(new SingleOption()
                {
                    Priority = 4,
                    Text = "Armor Shop",
                    Tooltip = "The armor shop stocks different types of armor for purchase. Closed at night.",
                    Select = GoToArmorShop,
                    Disabled = Party.Time == TimeOfDay.Night
                });
            }

            if (Party.Day < 30)
            {
                _eventOptions.Add(new SingleOption()
                {
                    Priority = 998,
                    Text = "Wait in Town",
                    Tooltip = "Wait a while in town.",
                    Select = WaitInTown
                });
            }

            _eventOptions.Add(new SingleOption()
            {
                Priority = 999,
                Text = "Leave Town",
                Tooltip = "Leave town and head back to the overworld.",
                Select = LeaveTown
            });
        }

        private void GoToInn()
        {
            IState innState = new TownInnState(
                Data.InnCost);
            _stateMachine.ChangeState(innState);
        }

        private void GoToDojo()
        {
            IState dojoState = new TownDojoState(
                Data.DojoCost);
            _stateMachine.ChangeState(dojoState);
        }

        private void GoToItemShop()
        {
            IState shopState = new TownShopState(
                "General Store",
                Data.ItemShopCost,
                Data.ItemShopInventory);
            _stateMachine.ChangeState(shopState);
        }

        private void GoToWeaponShop()
        {
            IState shopState = new TownShopState(
                "Weapon Shop",
                Data.WeaponShopCost,
                Data.WeaponShopInventory);
            _stateMachine.ChangeState(shopState);
        }

        private void GoToArmorShop()
        {
            IState shopState = new TownShopState(
                "Armor Shop",
                Data.ArmorShopCost,
                Data.ArmorShopInventory);
            _stateMachine.ChangeState(shopState);
        }

        private void WaitInTown()
        {
            Party.IncrementTime();
            _stateMachine.ChangeState<TownHubState>();
        }

        private void LeaveTown()
        {
            _context.Clear<TownData>();
            _stateMachine.ChangeState<GameplayState>();
        }
    }
}