using System;
using System.Collections.Generic;
using System.Linq;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine.Item;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class TownData
    {
        private static readonly Random RANDOM = new Random();

        public bool HasInn { get; }
        public uint InnCost { get; }

        public bool HasDojo { get; }
        public uint DojoCost { get; }

        public bool HasItemShop { get; }
        public float ItemShopCost { get; }
        public List<ItemDefinition> ItemShopInventory { get; }

        public bool HasWeaponShop { get; }
        public float WeaponShopCost { get; }
        public List<ItemDefinition> WeaponShopInventory { get; }

        public bool HasArmorShop { get; }
        public float ArmorShopCost { get; }
        public List<ItemDefinition> ArmorShopInventory { get; }

        public TownData()
        {
            HasInn = RANDOM.NextDouble() < 0.75f;
            InnCost = (uint) (50 + RANDOM.Next(50));

            HasDojo = RANDOM.NextDouble() < 0.2f;
            DojoCost = (uint) (150 + RANDOM.Next(150));

            HasItemShop = RANDOM.NextDouble() < 0.75f;
            ItemShopCost = (float) (0.8f + RANDOM.NextDouble() * 0.4f);
            if (HasItemShop)
            {
                int itemShopInventoryCount = 5 + RANDOM.Next(10);
                List<ItemDefinition> itemShopInventory = new List<ItemDefinition>(itemShopInventoryCount);
                for (int i = 0; i < itemShopInventoryCount; i++)
                    itemShopInventory.Add(HackUtil.GetRandomItem());
                ItemShopInventory = itemShopInventory;
            }

            HasWeaponShop = RANDOM.NextDouble() < 0.5f;
            WeaponShopCost = (float) (0.8f + RANDOM.NextDouble() * 0.4f);
            if (HasWeaponShop)
            {
                int weaponShopInventoryCount = 3 + RANDOM.Next(7);
                List<ItemDefinition> weaponShopInventory = new List<ItemDefinition>(weaponShopInventoryCount);
                for (int i = 0; i < weaponShopInventoryCount; i++)
                    weaponShopInventory.Add(HackUtil.GetRandomWeapon());
                WeaponShopInventory = weaponShopInventory;
            }

            HasArmorShop = RANDOM.NextDouble() < 0.5f;
            ArmorShopCost = (float) (0.8f + RANDOM.NextDouble() * 0.4f);
            if (HasArmorShop)
            {
                int armorShopInventoryCount = 3 + RANDOM.Next(7);
                List<ItemDefinition> armorShopInventory = new List<ItemDefinition>(armorShopInventoryCount);
                for (int i = 0; i < armorShopInventoryCount; i++)
                    armorShopInventory.Add(HackUtil.GetRandomArmor());
                ArmorShopInventory = armorShopInventory;
            }
        }
    }
}