using Newtonsoft.Json;

namespace google_server_to_server_api
{
    public class GoogleDeveloperNotification
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("packageName")]
        public string PackageName { get; set; }

        [JsonProperty("testNotification")]
        public TestNotification Test { get; set; }

        [JsonProperty("oneTimeProductNotification")]
        public OneTimeProductNotification OneTimeProduct { get; set; }

        [JsonProperty("subscriptionNotification")]
        public SubscriptionNotification Subscription { get; set; }
    }

    public class TestNotification
    {
        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class OneTimeProductNotification
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("notificationType")]
        public OneTimeProductNotificationType NotificationType { get; set; }

        [JsonProperty("purchaseToken")]
        public string PurchaseToken { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }
    }

    public class SubscriptionNotification
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("notificationType")]
        public SubscriptionNotificationType NotificationType { get; set; }

        [JsonProperty("purchaseToken")]
        public string PurchaseToken { get; set; }

        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }
    }

    public enum OneTimeProductNotificationType
    {
        /// <summary>A one-time product was successfully purchased by a user.</summary>
        ONE_TIME_PRODUCT_PURCHASED = 1,
        /// <summary>A pending one-time product purchase has been canceled by the user.</summary>
        ONE_TIME_PRODUCT_CANCELED = 2,
    }
}
