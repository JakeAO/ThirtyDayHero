using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace ThirtyDayHero
{
    public static class HackUtil
    {
        private static IReadOnlyCollection<IIdTracked> _allDefinitions;

        private static IReadOnlyCollection<IIdTracked> GetAllDefinitions()
        {
            if (_allDefinitions == null)
            {
                _allDefinitions = Assembly
                    .GetAssembly(typeof(IIdTracked))
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

        public class ItemJsonConverter : JsonConverter<IItem>
        {
            public override void WriteJson(JsonWriter writer, IItem value, JsonSerializer serializer)
            {
                writer.WriteValue(value.Id);
            }

            public override IItem ReadJson(JsonReader reader, Type objectType, IItem existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                uint id = Convert.ToUInt32(reader?.Value ?? 0);
                return GetDefinition<IItem>(id);
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
                return GetDefinition<IWeapon>(id);
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
                return GetDefinition<IArmor>(id);
            }
        }

        public class ClassJsonConverter : Newtonsoft.Json.JsonConverter<ICharacterClass>
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