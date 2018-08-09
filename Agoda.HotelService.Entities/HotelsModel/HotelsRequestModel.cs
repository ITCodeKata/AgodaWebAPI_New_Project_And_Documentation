namespace Agoda.HotelService.Entities.HotelsModel
{
    using System.ComponentModel.DataAnnotations;
    using static Agoda.HotelService.Common.CommonEnum;

    /// <summary>
    /// Hotels Request Model
    /// </summary>
    public class HotelsRequestModel
    {
        /// <summary>
        /// Hotel Code {String}
        /// </summary>
        [Required]
        public string CityId { get; set; }

        /// <summary>
        /// Price Sort Direction{ Optional}
        /// </summary>
        [EnumDataType(typeof(SortDirection))]
        public SortDirection SortDirection { get; set; }
    }
}
