namespace Agoda.HotelService.Entities.RateLimiter
{
    /// <summary>
    /// Rate Limiter Data
    /// </summary>
    public class RateLimiterData
    {
        /// <summary>
        /// Token bearer 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Current size of bucket 
        /// </summary>
        public double CurrentBudget { get; set; }

        /// <summary>
        /// Last update time 
        /// </summary>
        public long LastUpdateTime { get; set; }

        /// <summary>
        /// FillRatePerMs
        /// </summary>
        public double FillRatePerMs;

        /// <summary>
        /// MaxBudget
        /// </summary>
        public double MaxBudget;
    }
}
