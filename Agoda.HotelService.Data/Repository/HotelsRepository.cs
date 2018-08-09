namespace Agoda.HotelService.Data.Repository
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Agoda.HotelService.Entities.HotelsModel;
    using Agoda.HotelService.Data.SingletonInstance;
    using static Agoda.HotelService.Common.CommonEnum;
    using System.Diagnostics;


    /// <summary>
    /// Hotels DB Repository
    /// </summary>
    public class HotelsRepository: IHotelsRepository
    {
        /// <summary>
        /// Get All Hotels Data
        /// </summary>
        /// <returns>IList</returns>
        public IList<HotelsResponseData> GetAllHotelsData()
        {
            List<HotelsResponseData> hotelsList = new List<HotelsResponseData>();
            try
            {
                var mapPath = HttpContext.Current.Server.MapPath("~/App_Data/HotelsData/");
                string filePath = Path.Combine(mapPath, "HotelsData.json");

                var hotelsJsonList = JsonConvert.DeserializeObject<List<HotelsJsonModel>>(File.ReadAllText(filePath));
                if (hotelsJsonList != null)
                {
                    hotelsList.AddRange(hotelsJsonList.Select(h =>
                            new HotelsResponseData
                            {
                                CityId = h.City,
                                HotelId = h.HotelId,
                                RoomName = h.Room,
                                Price = h.Price
                            })
                          );
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error {0}", ex.Message));
            }

            return hotelsList;
        }

        /// <summary>
        /// GetHotels By CityId
        /// </summary>
        /// <param name="requestModel">HotelsRequestModel</param>
        /// <param name="totalRecordCount">int</param>
        /// <returns></returns>
        public IList<HotelsResponseData> GetHotelsByCityId(HotelsRequestModel requestModel, out int totalRecordCount)
        {
            List<HotelsResponseData> response = new List<HotelsResponseData>();
            totalRecordCount = default(Int32);
            try
            {
                var hotelsData = SingletonHotelsData.GetInstance().GetHotelsData;

                if (hotelsData != null)
                {
                    response = (from h in hotelsData
                                where (h.CityId.Equals(requestModel.CityId, StringComparison.OrdinalIgnoreCase))
                                select new HotelsResponseData(SortDirection.Desc)
                                {
                                    CityId = h.CityId,
                                    HotelId = h.HotelId,
                                    RoomName = h.RoomName,
                                    Price = h.Price
                                })
                                    .ToList();

                    response.Sort();

                    totalRecordCount = (response != null) ? Convert.ToInt32(response.Count()) : 0;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error {0}", ex.Message));
            }

            return response;
        }
    }
}
