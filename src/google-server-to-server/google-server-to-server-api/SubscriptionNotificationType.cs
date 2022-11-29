namespace google_server_to_server_api
{
    public enum SubscriptionNotificationType
    {
        /// <summary>A subscription was recovered from account hold.</summary>
        SUBSCRIPTION_RECOVERED = 1,
        /// <summary>An active subscription was renewed.</summary>
        SUBSCRIPTION_RENEWED = 2,
        /// <summary>A subscription was either voluntarily or involuntarily cancelled. For voluntary cancellation, sent when the user cancels.</summary>
        SUBSCRIPTION_CANCELED = 3,
        /// <summary>A new subscription was purchased.</summary>
        SUBSCRIPTION_PURCHASED = 4,
        /// <summary>A subscription has entered account hold(if enabled).</summary>
        SUBSCRIPTION_ON_HOLD = 5,
        /// <summary>A subscription has entered grace period(if enabled).</summary>
        SUBSCRIPTION_IN_GRACE_PERIOD = 6,
        /// <summary>User has restored their subscription from Play &gt; Account &gt; Subscriptions. 
        /// The subscription was canceled but had not expired yet when the user restores. 
        /// For more information, see: <see href="https://developer.android.com/google/play/billing/subscriptions#restore"/>.</summary>
        SUBSCRIPTION_RESTARTED = 7,
        /// <summary>A subscription price change has successfully been confirmed by the user.</summary>
        SUBSCRIPTION_PRICE_CHANGE_CONFIRMED = 8,
        /// <summary>A subscription's recurrence time has been extended.</summary>
        SUBSCRIPTION_DEFERRED = 9,
        /// <summary>A subscription has been paused.</summary>
        SUBSCRIPTION_PAUSED = 10,
        /// <summary>A subscription pause schedule has been changed.</summary>
        SUBSCRIPTION_PAUSE_SCHEDULE_CHANGED = 11,
        /// <summary>A subscription has been revoked from the user before the expiration time.</summary>
        SUBSCRIPTION_REVOKED = 12,
        /// <summary>A subscription has expired.</summary>
        SUBSCRIPTION_EXPIRED = 13,
    }
}
