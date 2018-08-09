namespace Agoda.HotelService.Business.HotelsProvider
{
    using System;
    using System.Collections.Generic;
    using Agoda.HotelService.Data.Repository;
    using Agoda.HotelService.Entities.HotelsModel;

    /// <summary>
    /// Hotels Provider {Business Layer}
    /// </summary>
    public class HotelsProvider
    {
        /// <summary>
        /// Get Hotels By CityId
        /// </summary>
        /// <param name="requestModel">HotelsRequestModel</param>
        /// <param name="totalRecordCount">int</param>
        /// <returns></returns>
        public static IList<HotelsResponseData> GetHotelsByCityId(HotelsRequestModel requestModel, out int totalRecordCount)
        {
            totalRecordCount = default(int);
            try
            {
                return new HotelsRepository().GetHotelsByCityId(requestModel, out totalRecordCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
