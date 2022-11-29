using System.Globalization;

namespace google_server_to_server_api
{
    public class NotificationProcessor : INotificationProcessor
    {
        private readonly IAndroidPublisherServiceWrapper _publisherService;

        public NotificationProcessor(IAndroidPublisherServiceWrapper publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<PurchaseChange> Process(GoogleNotification googleNotification)
        {
            if (string.IsNullOrWhiteSpace(googleNotification?.Message?.Data))
            {
                return null;
            }

            var notification = EncodedDeserializer.DeserializeFromBase64<GoogleDeveloperNotification>(googleNotification.Message.Data);

            if (notification?.Subscription == null) return null;

            var subscription = await _publisherService.GetPurchaseSubscriptionAsync(
                notification.Subscription.SubscriptionId,
                notification.Subscription.PurchaseToken);

            if (subscription == null) throw new ArgumentException("No such subscription", nameof(notification));

            // see: https://developer.android.com/google/play/billing/subscriptions
            PurchaseChange result = notification.Subscription.NotificationType switch
            {
                SubscriptionNotificationType.SUBSCRIPTION_PURCHASED => !string.IsNullOrEmpty(subscription.LinkedPurchaseToken)
                    ? new PurchaseUpgrade { Plan = notification.Subscription.SubscriptionId } : null, // handled when app sends registration request
                SubscriptionNotificationType.SUBSCRIPTION_RENEWED => new PurchaseRenewal { Plan = notification.Subscription.SubscriptionId },
                SubscriptionNotificationType.SUBSCRIPTION_IN_GRACE_PERIOD => new PurchaseGracePeriod { Expiration = subscription.ExpiryTimeMillis.ToDateTime() },
                SubscriptionNotificationType.SUBSCRIPTION_EXPIRED => new PurchaseExpire(),
                SubscriptionNotificationType.SUBSCRIPTION_PAUSED => new PurchasePause(),
                SubscriptionNotificationType.SUBSCRIPTION_RESTARTED => new PurchaseResume(),
                SubscriptionNotificationType.SUBSCRIPTION_ON_HOLD => null, // grace period notification took care of that
                SubscriptionNotificationType.SUBSCRIPTION_RECOVERED => new PurchaseResume(),
                SubscriptionNotificationType.SUBSCRIPTION_CANCELED => new PurchaseCancel(),
                SubscriptionNotificationType.SUBSCRIPTION_REVOKED => new PurchaseExpire(),
                SubscriptionNotificationType.SUBSCRIPTION_DEFERRED => null, // delay billing; we don't bill, so it's a NOP
                SubscriptionNotificationType.SUBSCRIPTION_PAUSE_SCHEDULE_CHANGED => null, // will receive SUBSCRIPTION_PAUSED
                SubscriptionNotificationType.SUBSCRIPTION_PRICE_CHANGE_CONFIRMED => null, // we have noting to do
                _ => throw new ArgumentException($"Unsupported notification type: {notification.Subscription.NotificationType}", nameof(notification))
            };

            if (subscription.AcknowledgementState == 0) await _publisherService.AcknowledgePurchaseSubscriptionAsync(
                subscription.ETag,
                notification.Subscription.SubscriptionId,
                notification.Subscription.PurchaseToken);

            if (result != null)
            {
                result.Source = "Google";
                result.PurchaseId = subscription.LinkedPurchaseToken;
                result.NewPurchaseId = notification.Subscription.PurchaseToken;
            }

            return result;
        }
    }

    public interface INotificationProcessor
    {
        Task<PurchaseChange> Process(GoogleNotification googleNotification);
    }

    public static class Extensions
    {
        public static DateTime? ToDateTime(this long? value) => value == null ? null
            : DateTimeOffset.FromUnixTimeMilliseconds(value.Value).UtcDateTime;
        public static DateTime ToDateTime(this long value) =>
            DateTimeOffset.FromUnixTimeMilliseconds(value).UtcDateTime;

        public static string ToFixedString(this decimal value) => value.ToString("F", CultureInfo.InvariantCulture);
    }
}
