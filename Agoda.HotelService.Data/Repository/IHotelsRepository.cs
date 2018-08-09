namespace Agoda.HotelService.Data.Repository
{
    using System.Collections.Generic;
    using Agoda.HotelService.Entities.HotelsModel;

    public interface IHotelsRepository
    {
        IList<HotelsResponseData> GetAllHotelsData();

        IList<HotelsResponseData> GetHotelsByCityId(HotelsRequestModel requestModel, out int totalRecordCount);

    }
}
