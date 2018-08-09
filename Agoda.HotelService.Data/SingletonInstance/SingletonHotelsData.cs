namespace Agoda.HotelService.Data.SingletonInstance
{
    using System.Collections.Generic;
    using Agoda.HotelService.Data.Repository;
    using Agoda.HotelService.Entities.HotelsModel;

    /// <summary>
    /// The 'Singleton'  class that returns only one object.
    /// </summary>
    public sealed class SingletonHotelsData
    {
        //create a mutex object to lock shared statement in GetInstance
        //method
        private static readonly object mutex = new object();
        private static SingletonHotelsData instance = null;
        private IList<HotelsResponseData> listHotelsResponseData { get; set; }

        /// <summary>
        /// Private constructor
        /// </summary>
        private SingletonHotelsData()
        {
            listHotelsResponseData = new HotelsRepository().GetAllHotelsData();
        }

        /// <summary>
        /// Get instance of HotelsData class
        /// Thread safe
        /// </summary>
        /// <returns></returns>
        public static SingletonHotelsData GetInstance()
        {
            if (instance == null)
            {
                lock (mutex)
                {
                    if (instance == null)
                    {
                        instance = new SingletonHotelsData();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Get Hotels Data
        /// </summary>
        public IList<HotelsResponseData> GetHotelsData
        {
            get
            {
                return listHotelsResponseData;
            }
        }
    }
}
