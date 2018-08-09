namespace Agoda.HotelService.Common
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Application Constant
    /// </summary>
    public class ApplicationConstant
    {
        /// <summary>
        /// Bucket maximum request capacity
        /// </summary>
        public readonly static double MAX_BUCKET_SIZE = Double.Parse(ConfigurationManager.AppSettings["Bucket.MaxBucketSize"]);

        /// <summary>
        /// Maximum time window in sec
        /// </summary>
        public readonly static double MAX_TIMEWINDOW_IN_SEC = Double.Parse(ConfigurationManager.AppSettings["Bucket.MaxTimeWindowInSec"]);

        /// <summary>
        /// Display custom message to user if RateLimit is applied
        /// </summary>
        public const string RATE_LIMIT_APPLIED = "Request exceeded more than RateLimit configured!";
    }
}
