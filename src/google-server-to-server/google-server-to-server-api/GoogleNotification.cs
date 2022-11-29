using Newtonsoft.Json;

namespace google_server_to_server_api
{
    public class GoogleNotification
    {
        [JsonProperty("message")]
        public GoogleNotificationMessage Message { get; set; }

        [JsonProperty("subscription")]
        public string Subscription { get; set; }
    }

    public class GoogleNotificationMessage
    {
        [JsonProperty("attributes")]
        public IDictionary<string, string> Attributes { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        [JsonProperty("message_id")]
        public string Message_Id { get; set; }

        [JsonProperty("publishTime")]
        public DateTime PublishTime { get; set; }

        [JsonProperty("publish_time")]
        public DateTime Publish_Time { get; set; }
    }
}
