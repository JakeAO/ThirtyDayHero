using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Armors;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.CharacterClasses;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Items;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons;
using SadPumpkin.Games.ThirtyDayHero.Core.Utilities;
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

        private static IReadOnlyDictionary<PlayerClassDefinition, double> _allPlayerClasses;
        public static PlayerClassDefinition GetRandomPlayerClass()
        {
            if (_allPlayerClasses == null)
            {
                int classCount = (int) (CharacterDefinitions.IdTracker.Current - CharacterDefinitions.IdTracker.Min);
                Dictionary<PlayerClassDefinition, double> allPlayerClasses = new Dictionary<PlayerClassDefinition, double>(classCount);
                for (uint i = CharacterDefinitions.IdTracker.Min; i < CharacterDefinitions.IdTracker.Current; i++)
                {
                    PlayerClassDefinition playerClass = GetDefinition<PlayerClassDefinition>(i);
                    if (playerClass != null)
                    {
                        allPlayerClasses[playerClass] = (double) playerClass.Rarity;
                    }
                }

                _allPlayerClasses = allPlayerClasses;
            }

            return RandomResultGenerator.Get(_allPlayerClasses);
        }

        private static IReadOnlyDictionary<EnemyDefinition, double> _allCalamityClasses;
        public static EnemyDefinition GetRandomCalamityClass()
        {
            if (_allCalamityClasses == null)
            {
                int classCount = (int) (CalamityDefinitions.IdTracker.Current - CalamityDefinitions.IdTracker.Min);
                Dictionary<EnemyDefinition, double> allCalamityClasses = new Dictionary<EnemyDefinition, double>(classCount);
                for (uint i = CalamityDefinitions.IdTracker.Min; i < CalamityDefinitions.IdTracker.Current; i++)
                {
                    EnemyDefinition calamityClass = GetDefinition<EnemyDefinition>(i);
                    if (calamityClass != null)
                    {
                        allCalamityClasses[calamityClass] = (double) calamityClass.Rarity;
                    }
                }

                _allCalamityClasses = allCalamityClasses;
            }

            return RandomResultGenerator.Get(_allCalamityClasses);
        }

        private static IReadOnlyDictionary<EnemyDefinition, double> _allEnemyClasses;
        public static EnemyDefinition GetRandomMonsterClass()
        {
            if (_allEnemyClasses == null)
            {
                int classCount = (int) (MonsterDefinitions.IdTracker.Current - MonsterDefinitions.IdTracker.Min);
                Dictionary<EnemyDefinition, double> allEnemyClasses = new Dictionary<EnemyDefinition, double>(classCount);
                for (uint i = MonsterDefinitions.IdTracker.Min; i < MonsterDefinitions.IdTracker.Current; i++)
                {
                    EnemyDefinition enemyClass = GetDefinition<EnemyDefinition>(i);
                    if (enemyClass != null)
                    {
                        allEnemyClasses[enemyClass] = (double) enemyClass.Rarity;
                    }
                }

                _allEnemyClasses = allEnemyClasses;
            }

            return RandomResultGenerator.Get(_allEnemyClasses);
        }

        private static IReadOnlyDictionary<EnemyGroup, double> _allEnemyGroups;
        
        public static EnemyGroup GetRandomEnemyGroup()
        {
            if (_allEnemyGroups == null)
            {
                int classCount = (int) (EnemyGroupDefinitions.IdTracker.Current - EnemyGroupDefinitions.IdTracker.Min);
                Dictionary<EnemyGroup, double> allEnemyGroups = new Dictionary<EnemyGroup, double>(classCount);
                for (uint i = EnemyGroupDefinitions.IdTracker.Min; i < EnemyGroupDefinitions.IdTracker.Current; i++)
                {
                    EnemyGroup enemyGroup = GetDefinition<EnemyGroup>(i);
                    if (enemyGroup != null)
                    {
                        allEnemyGroups[enemyGroup] = (double) enemyGroup.Rarity;
                    }
                }

                _allEnemyGroups = allEnemyGroups;
            }

            return RandomResultGenerator.Get(_allEnemyGroups);
        }

        private static IReadOnlyDictionary<ItemDefinition<IWeapon>, double> _allWeaponDefinitions;
        public static ItemDefinition<IWeapon> GetRandomWeapon()
        {
            bool GetMinMaxIds(WeaponType weaponType, out uint min, out uint current)
            {
                min = current = 0u;
                switch (weaponType)
                {
                    case WeaponType.Sword:
                        min = SwordDefinitions.IdTracker.Min;
                        current = SwordDefinitions.IdTracker.Max;
                        break;
                    case WeaponType.GreatSword:
                        min = GreatSwordDefinitions.IdTracker.Min;
                        current = GreatSwordDefinitions.IdTracker.Max;
                        break;
                    case WeaponType.Axe:
                        min = AxeDefinitions.IdTracker.Min;
                        current = AxeDefinitions.IdTracker.Max;
                        break;
                    case WeaponType.GreatAxe:
                        min = GreatAxeDefinitions.IdTracker.Min;
                        current = GreatAxeDefinitions.IdTracker.Max;
                        break;
                    case WeaponType.Spear:
                        min = SpearDefinitions.IdTracker.Min;
                        current = SpearDefinitions.IdTracker.Max;
                        break;
                    case WeaponType.Staff:
                        min = StaffDefinitions.IdTracker.Min;
                        current = StaffDefinitions.IdTracker.Max;
                        break;
                    case WeaponType.Rod:
                        min = RodDefinitions.IdTracker.Min;
                        current = RodDefinitions.IdTracker.Max;
                        break;
                    case WeaponType.Bow:
                        min = BowDefinitions.IdTracker.Min;
                        current = BowDefinitions.IdTracker.Max;
                        break;
                    case WeaponType.Fist:
                        min = FistDefinitions.IdTracker.Min;
                        current = FistDefinitions.IdTracker.Max;
                        break;
                }

                return min != 0u && current != 0u;
            }

            if (_allWeaponDefinitions == null)
            {
                Dictionary<ItemDefinition<IWeapon>, double> allWeaponDefinitions = new Dictionary<ItemDefinition<IWeapon>, double>(50);
                foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
                {
                    if (GetMinMaxIds(weaponType, out uint min, out uint current))
                    {
                        for (uint i = min; i < current; i++)
                        {
                            ItemDefinition<IWeapon> itemDefinition = GetDefinition<ItemDefinition<IWeapon>>(i);
                            if (itemDefinition != null)
                            {
                                allWeaponDefinitions[itemDefinition] = (double) itemDefinition.Rarity;
                            }
                        }
                    }
                }

                _allWeaponDefinitions = allWeaponDefinitions;
            }

            return RandomResultGenerator.Get(_allWeaponDefinitions);
        }

        private static IReadOnlyDictionary<ItemDefinition<IArmor>, double> _allArmorDefinitions;
        public static ItemDefinition<IArmor> GetRandomArmor()
        {
            bool GetMinMaxIds(ArmorType armorType, out uint min, out uint current)
            {
                min = current = 0u;
                switch (armorType)
                {
                    case ArmorType.Light:
                        min = LightDefinitions.IdTracker.Min;
                        current = LightDefinitions.IdTracker.Current;
                        break;
                    case ArmorType.Medium:
                        min = MediumDefinitions.IdTracker.Min;
                        current = MediumDefinitions.IdTracker.Current;
                        break;
                    case ArmorType.Heavy:
                        min = HeavyDefinitions.IdTracker.Min;
                        current = HeavyDefinitions.IdTracker.Current;
                        break;
                }

                return min != 0u && current != 0u;
            }

            if (_allArmorDefinitions == null)
            {
                Dictionary<ItemDefinition<IArmor>, double> allArmorDefinitions = new Dictionary<ItemDefinition<IArmor>, double>(50);
                foreach (ArmorType armorType in Enum.GetValues(typeof(ArmorType)))
                {
                    if (GetMinMaxIds(armorType, out uint min, out uint current))
                    {
                        for (uint i = min; i < current; i++)
                        {
                            ItemDefinition<IArmor> itemDefinition = GetDefinition<ItemDefinition<IArmor>>(i);
                            if (itemDefinition != null)
                            {
                                allArmorDefinitions[itemDefinition] = (double) itemDefinition.Rarity;
                            }
                        }
                    }
                }

                _allArmorDefinitions = allArmorDefinitions;
            }

            return RandomResultGenerator.Get(_allArmorDefinitions);
        }

        private static IReadOnlyDictionary<ItemDefinition<IItem>, double> _allItemDefinitions;
        public static ItemDefinition<IItem> GetRandomItem()
        {
            bool GetMinMaxIds(ItemType itemType, out uint min, out uint current)
            {
                min = current = 0u;
                switch (itemType)
                {
                    case ItemType.Trinket:
                        min = TrinketDefinitions.IdTracker.Min;
                        current = TrinketDefinitions.IdTracker.Current;
                        break;
                    case ItemType.Loot:
                        min = LootDefinitions.IdTracker.Min;
                        current = LootDefinitions.IdTracker.Current;
                        break;
                    case ItemType.Consumable:
                        min = ConsumableDefinitions.IdTracker.Min;
                        current = ConsumableDefinitions.IdTracker.Current;
                        break;
                }

                return min != 0u && current != 0u;
            }

            if (_allItemDefinitions == null)
            {
                Dictionary<ItemDefinition<IItem>, double> allItemDefinitions = new Dictionary<ItemDefinition<IItem>, double>(50);
                foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
                {
                    if (GetMinMaxIds(itemType, out uint min, out uint current))
                    {
                        for (uint i = min; i < current; i++)
                        {
                            ItemDefinition<IItem> itemDefinition = GetDefinition<ItemDefinition<IItem>>(i);
                            if (itemDefinition != null)
                            {
                                allItemDefinitions[itemDefinition] = (double) itemDefinition.Rarity;
                            }
                        }
                    }
                }

                _allItemDefinitions = allItemDefinitions;
            }

            return RandomResultGenerator.Get(_allItemDefinitions);
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