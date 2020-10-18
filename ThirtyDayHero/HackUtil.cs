using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Armors;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.CharacterClasses;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Items;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons;
using SadPumpkin.Util.CombatEngine;
using SadPumpkin.Util.CombatEngine.CharacterClasses;
using SadPumpkin.Util.CombatEngine.Item;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core
{
    public static class HackUtil
    {
        private static readonly Random RANDOM = new Random();
        
        private static IReadOnlyCollection<JsonConverter> _allJsonConverters;

        public static IReadOnlyCollection<JsonConverter> GetAllJsonConverters()
        {
            if (_allJsonConverters == null)
            {
                _allJsonConverters = Assembly
                    .GetAssembly(typeof(HackUtil))
                    .GetTypes()
                    .Where(x =>
                        !x.IsAbstract &&
                        x.IsClass &&
                        typeof(JsonConverter).IsAssignableFrom(x))
                    .Select(x => x.GetConstructor(Type.EmptyTypes))
                    .Where(x => x != null)
                    .Select(x => (JsonConverter) x.Invoke(new object[0]))
                    .ToArray();
            }

            return _allJsonConverters;
        }

        private static IReadOnlyCollection<IIdTracked> _allDefinitions;
        
        private static IReadOnlyCollection<IIdTracked> GetAllDefinitions()
        {
            if (_allDefinitions == null)
            {
                _allDefinitions = Assembly
                    .GetAssembly(typeof(HackUtil))
                    .GetTypes()
                    .Where(x =>
                        x.IsAbstract &&
                        x.IsSealed &&
                        x.IsClass &&
                        x.Name.Contains("Definitions"))
                    .SelectMany(x => x.GetFields(BindingFlags.Public | BindingFlags.Static))
                    .Where(x => typeof(IIdTracked).IsAssignableFrom(x.FieldType))
                    .Select(x => (IIdTracked) x.GetValue(null))
                    .ToArray();
            }

            return _allDefinitions;
        }

        public static T GetDefinition<T>(uint id) where T : class, IIdTracked
        {
            foreach (IIdTracked tracked in GetAllDefinitions())
            {
                if (tracked is T typedObj &&
                    tracked.Id == id)
                {
                    return typedObj;
                }
            }

            return default;
        }

        public static string GetRandomCharacterName()
        {
            int randomIdx = RANDOM.Next(0, CharacterNames.NAMES.Count);
            return CharacterNames.NAMES[randomIdx];
        }
        
        public static IPlayerClass GetRandomPlayerClass()
        {
            uint randId = (uint) RANDOM.Next((int) CharacterDefinitions.IdTracker.Min, (int) CharacterDefinitions.IdTracker.Current);
            return GetDefinition<IPlayerClass>(randId);
        }

        public static ICharacterClass GetRandomCalamityClass()
        {
            uint randId = (uint) RANDOM.Next((int) CalamityDefinitions.IdTracker.Min, (int) CalamityDefinitions.IdTracker.Current);
            return GetDefinition<ICharacterClass>(randId);
        }

        public static ICharacterClass GetRandomMonsterClass()
        {
            uint randId = (uint) RANDOM.Next((int) MonsterDefinitions.IdTracker.Min, (int) MonsterDefinitions.IdTracker.Current);
            return GetDefinition<ICharacterClass>(randId);
        }

        public static IWeapon GetRandomWeapon()
        {
            uint randId = 0u;
            WeaponType[] types = Enum.GetValues(typeof(WeaponType)).Cast<WeaponType>().Except(new[] {WeaponType.Invalid}).ToArray();
            WeaponType randType = types[RANDOM.Next(0, types.Length)];
            switch (randType)
            {
                case WeaponType.Sword:
                    randId = (uint) RANDOM.Next((int) SwordDefinitions.IdTracker.Min, (int) SwordDefinitions.IdTracker.Current);
                    break;
                case WeaponType.GreatSword:
                    randId = (uint) RANDOM.Next((int) GreatSwordDefinitions.IdTracker.Min, (int) GreatSwordDefinitions.IdTracker.Current);
                    break;
                case WeaponType.Axe:
                    randId = (uint) RANDOM.Next((int) AxeDefinitions.IdTracker.Min, (int) AxeDefinitions.IdTracker.Current);
                    break;
                case WeaponType.GreatAxe:
                    randId = (uint) RANDOM.Next((int) GreatAxeDefinitions.IdTracker.Min, (int) GreatAxeDefinitions.IdTracker.Current);
                    break;
                case WeaponType.Spear:
                    randId = (uint) RANDOM.Next((int) SpearDefinitions.IdTracker.Min, (int) SpearDefinitions.IdTracker.Current);
                    break;
                case WeaponType.Staff:
                    randId = (uint) RANDOM.Next((int) StaffDefinitions.IdTracker.Min, (int) StaffDefinitions.IdTracker.Current);
                    break;
                case WeaponType.Rod:
                    randId = (uint) RANDOM.Next((int) RodDefinitions.IdTracker.Min, (int) RodDefinitions.IdTracker.Current);
                    break;
                case WeaponType.Bow:
                    randId = (uint) RANDOM.Next((int) BowDefinitions.IdTracker.Min, (int) BowDefinitions.IdTracker.Current);
                    break;
                case WeaponType.Fist:
                    randId = (uint) RANDOM.Next((int) FistDefinitions.IdTracker.Min, (int) FistDefinitions.IdTracker.Current);
                    break;
            }

            return GetDefinition<IWeapon>(randId);
        }

        public static IArmor GetRandomArmor()
        {
            uint randId = 0u;
            ArmorType[] types = Enum.GetValues(typeof(ArmorType)).Cast<ArmorType>().Except(new[] {ArmorType.Invalid}).ToArray();
            ArmorType randType = types[RANDOM.Next(0, types.Length)];
            switch (randType)
            {
                case ArmorType.Light:
                    randId = (uint) RANDOM.Next((int) LightDefinitions.IdTracker.Min, (int) LightDefinitions.IdTracker.Current);
                    break;
                case ArmorType.Medium:
                    randId = (uint) RANDOM.Next((int) MediumDefinitions.IdTracker.Min, (int) MediumDefinitions.IdTracker.Current);
                    break;
                case ArmorType.Heavy:
                    randId = (uint) RANDOM.Next((int) HeavyDefinitions.IdTracker.Min, (int) HeavyDefinitions.IdTracker.Current);
                    break;
            }

            return GetDefinition<IArmor>(randId);
        }

        public static IItem GetRandomItem()
        {
            uint randId = 0u;
            ItemType[] types = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Except(new[] {ItemType.Invalid, ItemType.Weapon, ItemType.Armor}).ToArray();
            ItemType type = types[RANDOM.Next(0, types.Length)];
            switch (type)
            {
                case ItemType.Trinket:
                    randId = (uint) RANDOM.Next((int) TrinketDefinitions.IdTracker.Min, (int) TrinketDefinitions.IdTracker.Current);
                    break;
                case ItemType.Loot:
                    randId = (uint) RANDOM.Next((int) LootDefinitions.IdTracker.Min, (int) LootDefinitions.IdTracker.Current);
                    break;
                case ItemType.Consumable:
                    randId = (uint) RANDOM.Next((int) ConsumableDefinitions.IdTracker.Min, (int) ConsumableDefinitions.IdTracker.Current);
                    break;
            }

            return GetDefinition<IItem>(randId);
        }

        public class ItemJsonConverter : JsonConverter<IItem>
        {
            public override void WriteJson(JsonWriter writer, IItem value, JsonSerializer serializer)
            {
                writer.WriteValue(value.Id);
            }

            public override IItem ReadJson(JsonReader reader, Type objectType, IItem existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                uint id = Convert.ToUInt32(reader?.Value ?? 0);
                IItem item = GetDefinition<IItem>(id);
                return item;
            }
        }

        public class WeaponJsonConverter : JsonConverter<IWeapon>
        {
            public override void WriteJson(JsonWriter writer, IWeapon value, JsonSerializer serializer)
            {
                writer.WriteValue(value.Id);
            }

            public override IWeapon ReadJson(JsonReader reader, Type objectType, IWeapon existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                uint id = Convert.ToUInt32(reader?.Value ?? 0);
                IWeapon item = GetDefinition<IWeapon>(id);
                return item;
            }
        }

        public class ArmorJsonConverter : JsonConverter<IArmor>
        {
            public override void WriteJson(JsonWriter writer, IArmor value, JsonSerializer serializer)
            {
                writer.WriteValue(value.Id);
            }

            public override IArmor ReadJson(JsonReader reader, Type objectType, IArmor existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                uint id = Convert.ToUInt32(reader?.Value ?? 0);
                IArmor item = GetDefinition<IArmor>(id);
                return item;
            }
        }

        public class ClassJsonConverter : JsonConverter<ICharacterClass>
        {
            public override void WriteJson(JsonWriter writer, ICharacterClass value, JsonSerializer serializer)
            {
                writer.WriteValue(value.Id);
            }

            public override ICharacterClass ReadJson(JsonReader reader, Type objectType, ICharacterClass existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                uint id = Convert.ToUInt32(reader?.Value ?? 0);
                return GetDefinition<ICharacterClass>(id);
            }
        }
    }
}