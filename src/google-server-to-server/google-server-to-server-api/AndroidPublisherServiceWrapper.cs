using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace google_server_to_server_api
{
    public class AndroidPublisherServiceWrapper: IAndroidPublisherServiceWrapper
    {
        private readonly VerifierOptions _verifierOptions;
        private readonly AndroidPublisherService _publisherService;

        public AndroidPublisherServiceWrapper(
            IOptions<VerifierOptions> verifierOptions)
        {
            _verifierOptions = verifierOptions.Value;

            var serviceAccountStr = JsonConvert.SerializeObject(_verifierOptions.ServiceAccount);

            var initializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential
                    .FromJson(serviceAccountStr)
                    .CreateScoped(AndroidPublisherService.Scope.Androidpublisher)
            };

            _publisherService = new AndroidPublisherService(initializer);
        }

        public Task<string> AcknowledgePurchaseSubscriptionAsync(
            string eTag,
            string subscriptionId,
            string purchaseToken,
            CancellationToken cancellationToken = default) => _publisherService.Purchases.Subscriptions
                .Acknowledge(new() { ETag = eTag }, _verifierOptions.PackageName, subscriptionId, purchaseToken)
                .ExecuteAsync(cancellationToken);

        public Task<SubscriptionPurchase> GetPurchaseSubscriptionAsync(
            string subscriptionId,
            string purchaseToken,
            CancellationToken cancellationToken = default) => _publisherService.Purchases.Subscriptions
                .Get(_verifierOptions.PackageName, subscriptionId, purchaseToken)
                .ExecuteAsync(cancellationToken);
    }

    public interface IAndroidPublisherServiceWrapper
    {
        Task<SubscriptionPurchase> GetPurchaseSubscriptionAsync(
            string subscriptionId,
            string purchaseToken,
            CancellationToken cancellationToken = default);

        Task<string> AcknowledgePurchaseSubscriptionAsync(
            string eTag,
            string subscriptionId,
            string purchaseToken,
            CancellationToken cancellationToken = default);
    }
}
