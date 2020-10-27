using System;
using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine.Item;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States.Town
{
    public class TownShopState : BlazorState
    {
        public override Type RenderType => typeof(TownShopStatePage);

        public event EventHandler ShopUpdated;
        
        public PartyDataWrapper Party { get; private set; }
        public string ShopName { get; }
        public float ShopCostModifier { get; }
        public IReadOnlyList<(ItemDefinition, uint)> BuyList => _buyList;
        public IReadOnlyList<(ItemDefinition, uint)> SellList => _sellList;

        private List<(ItemDefinition, uint)> _buyList = null;
        private List<(ItemDefinition, uint)> _sellList = null;

        private readonly List<ItemDefinition> _shopInventory = null;
        
        public TownShopState(
            string shopName,
            float shopCostModifier,
            List<ItemDefinition> shopInventory)
        {
            ShopName = shopName;
            ShopCostModifier = shopCostModifier;

            _shopInventory = shopInventory;
        }

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            Party = context.Get<PartyDataWrapper>();

            _buyList = new List<(ItemDefinition, uint)>(_shopInventory.Count);
            foreach (ItemDefinition itemDefinition in _shopInventory)
            {
                _buyList.Add((itemDefinition, CalculatePurchasePrice(itemDefinition)));
            }

            _buyList = _buyList
                .OrderBy(x => x.Item1.Item.ItemType)
                .ThenBy(x =>
                {
                    switch (x.Item1.Item)
                    {
                        case IWeapon weapon: return (int) weapon.WeaponType;
                        case IArmor armor: return (int) armor.ArmorType;
                        default: return 0;
                    }
                })
                .ThenBy(x => x.Item1.Rarity)
                .ThenBy(x => x.Item1.Value)
                .ToList();

            _sellList = new List<(ItemDefinition, uint)>(Party.Inventory.Count);
            foreach (IItem item in Party.Inventory)
            {
                ItemDefinition itemDef = HackUtil.GetDefinition<ItemDefinition>(item.Id);
                if (itemDef != null)
                {
                    _sellList.Add((itemDef, CalculateSalePrice(itemDef)));
                }
            }

            _sellList = _sellList
                .OrderBy(x => x.Item1.Item.ItemType)
                .ThenBy(x =>
                {
                    switch (x.Item1.Item)
                    {
                        case IWeapon weapon: return (int) weapon.WeaponType;
                        case IArmor armor: return (int) armor.ArmorType;
                        default: return 0;
                    }
                })
                .ThenBy(x => x.Item1.Rarity)
                .ThenBy(x => x.Item1.Value)
                .ToList();

            ShopUpdated?.Invoke(this, EventArgs.Empty);
        }

        public bool CanBuy((ItemDefinition, uint) itemAndPrice)
        {
            if (!_buyList.Contains(itemAndPrice))
                return false;
            if (itemAndPrice.Item2 > Party.Gold)
                return false;
            return true;
        }

        public bool BuyItem((ItemDefinition, uint) itemAndPrice)
        {
            if (!CanBuy(itemAndPrice))
                return false;

            Party.Gold -= itemAndPrice.Item2;
            Party.Inventory.Add(itemAndPrice.Item1.Item);

            _buyList.Remove(itemAndPrice);

            // Calculate new Sale price of the purchased item
            _sellList.Add((itemAndPrice.Item1, CalculateSalePrice(itemAndPrice.Item1)));

            ShopUpdated?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool CanSell((ItemDefinition, uint) itemAndPrice)
        {
            if (!_sellList.Contains(itemAndPrice))
                return false;
            if (!Party.Inventory.Contains(itemAndPrice.Item1.Item))
                return false;
            return true;
        }

        public bool SellItem((ItemDefinition, uint) itemAndPrice)
        {
            if (!CanSell(itemAndPrice))
                return false;

            Party.Gold += itemAndPrice.Item2;
            Party.Inventory.Remove(itemAndPrice.Item1.Item);

            _sellList.Remove(itemAndPrice);

            // Calculate new Purchase price of the sold item
            _buyList.Add((itemAndPrice.Item1, CalculatePurchasePrice(itemAndPrice.Item1)));

            ShopUpdated?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void LeaveShop()
        {
            _context.Get<IStateMachine>().ChangeState<TownHubState>();
        }
        
        private uint CalculatePurchasePrice(ItemDefinition itemDefinition)
        {
            double shopPrice = itemDefinition.Value * ShopCostModifier;

            double avgPartyCha = Party.Characters.Average(x => x.Stats[StatType.CHA]);
            double chaPriceMod = 1f - 0.5f * avgPartyCha / 80f;

            return (uint) Math.Round(shopPrice * chaPriceMod);
        }

        private uint CalculateSalePrice(ItemDefinition itemDefinition)
        {
            double shopPrice = itemDefinition.Value * ShopCostModifier;

            double avgPartyCha = Party.Characters.Average(x => x.Stats[StatType.CHA]);
            double chaPriceMod = 0.5f + 0.5f * avgPartyCha / 80f;

            return (uint) Math.Round(shopPrice * chaPriceMod);
        }
    }
}