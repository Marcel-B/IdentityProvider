using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.marcelbenders.App.IdentityProvider.Model
{
    namespace WoMoDiary.Domain
    {
        public partial class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public long ExpiresIn { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }
        }

        public partial class Token
        {
            public static Token FromJson(string json) => JsonConvert.DeserializeObject<Token>(json, WoMoDiary.Domain.Converter.Settings);
        }

        public static class Serialize
        {
            public static string ToJson(this Token self) => JsonConvert.SerializeObject(self, WoMoDiary.Domain.Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                },
            };
        }
    }
}