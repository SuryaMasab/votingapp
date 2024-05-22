using System.Text.Json;

namespace VoteBlazor.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient AddJsonOptions(this HttpClient client, Action<JsonSerializerOptions> configure)
        {
            var options = new JsonSerializerOptions();
            configure(options);

            // configure headers content type
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            return client;
        }
    }
}
