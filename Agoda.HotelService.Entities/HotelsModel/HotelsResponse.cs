namespace Agoda.HotelService.Entities.HotelsModel
{
    using Agoda.HotelService.Entities.CommonModel;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using static Agoda.HotelService.Common.CommonEnum;

    /// <summary>
    /// HotelsResponse model is used for external application intraction 
    /// </summary>
    [DataContract]
    public class HotelsResponse : CommonResponseModel
    {
        /// <summary>
        /// Hotels List
        /// </summary>
        [DataMember]
        public IList<HotelsResponseData> Response { get; set; }
        /// <summary>
        /// Total Hotels Count
        /// </summary>
        [DataMember]
        public int TotalRecordCount { get; set; }
    }

    /// <summary>
    /// HotelsResponse data
    /// </summary>
    [DataContract]
    public class HotelsResponseData : IComparable<HotelsResponseData>
    {
        private readonly SortDirection _sortDirection = SortDirection.Asc;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="sortDirection">Asc</param>
        public HotelsResponseData(SortDirection sortDirection = SortDirection.Asc)
        {
            _sortDirection = sortDirection;
        }

        /// <summary>
        /// City Code
        /// </summary>
        [DataMember]
        public string CityId { get; set; }

        /// <summary>
        /// Hotels Code
        /// </summary>
        [DataMember]
        public int HotelId { get; set; }

        /// <summary>
        /// Hotels Room Name
        /// </summary>
        [DataMember]
        public string RoomName { get; set; }

        /// <summary>
        /// Hotels Room Price
        /// </summary>
        [DataMember]
        public double Price { get; set; }

        /// <summary>
        /// Implemented contract for IComparable
        /// Sorting
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(HotelsResponseData other)
        {
            int result = this.Price.CompareTo(other.Price);
            if (_sortDirection == SortDirection.Desc) result *= -1;
            return result;
        }
    }
}
