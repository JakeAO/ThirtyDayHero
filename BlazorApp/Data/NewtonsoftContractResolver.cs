using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class NewtonsoftContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = obj =>
                {
                    IEnumerable enumerable = null;
                    switch (member)
                    {
                        case PropertyInfo propertyInfo:
                            enumerable = (IEnumerable) propertyInfo.GetValue(obj);
                            break;
                        case FieldInfo fieldInfo:
                            enumerable = (IEnumerable) fieldInfo.GetValue(obj);
                            break;
                    }
                    
                    bool anyElements = enumerable?.GetEnumerator().MoveNext() ?? false;

                    return anyElements;
                };
            }

            return property;
        }
    }
}