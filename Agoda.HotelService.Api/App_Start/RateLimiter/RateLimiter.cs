namespace Agoda.HotelService.Api.App_Start.RateLimiter
{
    using System;
    using Agoda.HotelService.Common;
    using Agoda.HotelService.Data.SingletonInstance;
    using Agoda.HotelService.Entities.RateLimiter;
 

    /// <summary>
    /// Rate Limiter
    /// </summary>
    public class RateLimiter
    {
        private readonly DictionaryRateLimiter _instance = null;
        private readonly double amount = default(double);
        private readonly double maxBucketSize = default(double);//Global Constant
        private readonly double maxTimeWindowInSec = default(double);

        /// <summary>
        /// Constructor 
        /// </summary>
        public RateLimiter()
        {
            _instance = DictionaryRateLimiter.GetInstance();
            amount = 1;
            maxBucketSize = ApplicationConstant.MAX_BUCKET_SIZE;
            maxTimeWindowInSec = ApplicationConstant.MAX_TIMEWINDOW_IN_SEC;
        }


        /// <summary>
        /// Apply Rate Limit 
        /// </summary>
        /// <param name="key">string</param>
        /// <param name="bucketSize">double</param>
        /// <returns></returns>
        public bool ApplyRateLimit(string key, double bucketSize)
        {
            bool _consumeRes = default(bool);
            var rateLimiter = _instance.GetRateLimiterByKey(key);

            if (rateLimiter != null)
            {
                _consumeRes = Consume(rateLimiter, out RateLimiterData rl);
                _instance.UpdateRateLimiterByKey(rl);
            }
            else
            {
                DateTimeOffset _dateTime = DateTimeOffset.Now;
                long maxTimeWindowInMilliseconds = _dateTime.AddSeconds(maxTimeWindowInSec).ToUnixTimeMilliseconds();

                var rateLimter = new RateLimiterData()
                {
                    Token = key,
                    LastUpdateTime = _dateTime.ToUnixTimeMilliseconds(),
                    FillRatePerMs = bucketSize / maxTimeWindowInMilliseconds,
                    CurrentBudget = bucketSize,
                    MaxBudget = bucketSize
                };
                _instance.UpdateRateLimiterByKey(rateLimter);
                _consumeRes = true;
            }

            return _consumeRes;
        }


        /// <summary>
        /// Attempt to consume the specified amount of resources.  If the resources
        /// are available, consume them and return true; otherwise, consume nothing
        /// and return false.
        /// </summary>
        /// <param name="rateLimiter">RateLimiterData</param>
        /// <param name="updatedRateLimiter">RateLimiterData</param>
        /// <returns></returns>
        public bool Consume(RateLimiterData rateLimiter, out RateLimiterData updatedRateLimiter)
        {
            bool _consumeRes = default(bool);
            double _currentBudget = rateLimiter.CurrentBudget;
            double _maxBudget = rateLimiter.MaxBudget;
            long _lastUpdateTime = rateLimiter.LastUpdateTime;
            double _fillRatePerMs = rateLimiter.FillRatePerMs;
            updatedRateLimiter = new RateLimiterData();

            /*
             * If key is suspended for BucketOverflow, return fasle
            **/
            if (_lastUpdateTime <= DateTimeOffset.Now.ToUnixTimeMilliseconds())
            {
                long msSinceLastUpdate = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _lastUpdateTime;
                _currentBudget = Math.Min(_maxBudget,
                    _currentBudget + msSinceLastUpdate * _fillRatePerMs);
                _lastUpdateTime += msSinceLastUpdate;


                updatedRateLimiter.Token = rateLimiter.Token;
                updatedRateLimiter.MaxBudget = rateLimiter.MaxBudget;
                updatedRateLimiter.FillRatePerMs = rateLimiter.FillRatePerMs;

                if (_currentBudget >= amount)
                {
                    _currentBudget -= amount;

                    updatedRateLimiter.CurrentBudget = _currentBudget;
                    updatedRateLimiter.MaxBudget = rateLimiter.MaxBudget;
                    updatedRateLimiter.LastUpdateTime = _lastUpdateTime;

                    _consumeRes = true;
                }
                else
                {
                    /*
                     * If key is suspended for BucketOverflow for next 5 min, return fasle
                    **/
                    _lastUpdateTime = DateTimeOffset.Now.AddMinutes(5).ToUnixTimeMilliseconds();
                    updatedRateLimiter.LastUpdateTime = _lastUpdateTime;
                    updatedRateLimiter.CurrentBudget = rateLimiter.MaxBudget;
                }
            }

            return _consumeRes;

        }
    }
}