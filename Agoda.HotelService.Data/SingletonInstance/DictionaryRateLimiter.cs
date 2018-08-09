namespace Agoda.HotelService.Data.SingletonInstance
{
    using System;
    using System.Collections.Generic;
    using Agoda.HotelService.Entities.RateLimiter;

    /// <summary>
    /// The 'Singleton'  class that returns only one object.
    /// </summary>
    public sealed class DictionaryRateLimiter
    {
        //create a mutex object to lock shared statement in GetInstance
        //method
        private static readonly object mutex = new object();
        private static DictionaryRateLimiter instance = null;
        private static Dictionary<string, RateLimiterData> dict { get; set; }

        /// <summary>
        /// Private constructor
        /// </summary>
        private DictionaryRateLimiter()
        {
            dict = new Dictionary<string, RateLimiterData>();
        }

        /// <summary>
        /// Get instance of HotelsData class
        /// Thread safe
        /// </summary>
        /// <returns></returns>
        public static DictionaryRateLimiter GetInstance()
        {
            if (instance == null)
            {
                lock (mutex)
                {
                    if (instance == null)
                    {
                        instance = new DictionaryRateLimiter();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Get RateLimiter
        /// </summary>
        public RateLimiterData GetRateLimiterByKey(string key)
        {
            RateLimiterData _rateLimiter = null;
            try
            {
                if (!string.IsNullOrEmpty(key) && dict.ContainsKey(key))
                {
                    _rateLimiter= dict[key];
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _rateLimiter;
        }

        /// <summary>
        /// Update RateLimiter
        /// </summary>
        public void UpdateRateLimiterByKey(RateLimiterData rateLimiter)
        {
            try
            {
                if (!string.IsNullOrEmpty(rateLimiter.Token))
                {
                    if (dict.ContainsKey(rateLimiter.Token))
                    {
                        dict.Remove(rateLimiter.Token);
                    }
                    dict.Add(rateLimiter.Token, rateLimiter);
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
