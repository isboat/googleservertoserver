namespace google_server_to_server_api
{
    using System.Text;

    using Microsoft.AspNetCore.WebUtilities;

    using Newtonsoft.Json;

    public static class EncodedDeserializer
    {
        public static TObj? DeserializeFromBase64<TObj>(string encodedString)
        {
            if (string.IsNullOrWhiteSpace(encodedString)) return default!;

            var data = Base64UrlTextEncoder.Decode(encodedString);
            string decodedString = Encoding.UTF8.GetString(data);

            return JsonConvert.DeserializeObject<TObj>(decodedString);
        }

        public static string? SerializeToBase64<TObj>(TObj obj)
            where TObj : class
        {
            if (obj == default) return null!;

            var serializedObject = JsonConvert.SerializeObject(obj);

            var data = Encoding.UTF8.GetBytes(serializedObject);
            return Base64UrlTextEncoder.Encode(data);
        }
    }
}
