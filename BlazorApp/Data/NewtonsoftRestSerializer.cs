using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class NewtonWrapper : IRestSerializer
    {
        private JsonSerializerSettings _settings = null;

        public NewtonWrapper(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

#pragma warning disable 618
        public string Serialize(Parameter parameter) => Serialize(parameter.Value);
#pragma warning restore 618
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj, _settings).Replace('$', '~');
        public T Deserialize<T>(IRestResponse response) => JsonConvert.DeserializeObject<T>(response.Content.Replace('~', '$'), _settings);

        public string ContentType { get; set; } = RestSharp.Serialization.ContentType.Json;
        public string[] SupportedContentTypes { get; } = RestSharp.Serialization.ContentType.JsonAccept;
        public DataFormat DataFormat { get; } = DataFormat.Json;
    }
}