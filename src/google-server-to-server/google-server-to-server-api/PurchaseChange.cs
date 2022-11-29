using System.Diagnostics.CodeAnalysis;

namespace google_server_to_server_api
{
    public abstract class PurchaseChange : PurchaseRef { }

    public sealed class PurchaseUpgrade : PurchaseChange
    {
        public string Plan { get; set; } = string.Empty;
    }

    public sealed class PurchaseActionCancelled : PurchaseChange
    {
        public string Plan { get; set; } = string.Empty;
    }

    public sealed class PurchaseDowngrade : PurchaseChange
    {
        public string Plan { get; set; } = string.Empty;
    }

    public sealed class PurchaseGracePeriod : PurchaseChange
    {
        public DateTime? Expiration { get; set; }
    }

    public sealed class PurchaseRenewal : PurchaseChange
    {
        public string Plan { get; set; } = string.Empty;
    }

    public sealed class PurchaseExpire : PurchaseChange { }

    public sealed class PurchasePause : PurchaseChange { }
    public sealed class PurchaseResume : PurchaseChange { }

    public sealed class PurchaseCancel : PurchaseChange { }
    public sealed class PurchaseRestore : PurchaseChange { }

    public abstract class PurchaseRef
    {
        private string? _newPurchaseId = null;


        /// <summary>
        /// The source system of the purchase (eg: 'Apple', 'Google')
        /// </summary>
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of purchase this record relates to
        /// </summary>
        public string PurchaseId { get; set; } = string.Empty;

        /// <summary>
        /// The Identifier of purchase this record represents
        /// </summary>
        [MaybeNull]
        public string NewPurchaseId
        {
            get => string.IsNullOrEmpty(_newPurchaseId) ? PurchaseId : _newPurchaseId;
            set => _newPurchaseId = string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
