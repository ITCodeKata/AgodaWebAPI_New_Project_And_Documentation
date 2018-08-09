namespace Agoda.HotelService.Entities.HotelsModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Hotels Json Model
    /// </summary>
    public class HotelsJsonModel
    {
        /// <summary>
        /// City Code
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Hotels Code
        /// </summary>
        [DataMember]
        public int HotelId { get; set; }

        /// <summary>
        /// Hotels Room Name
        /// </summary>
        [DataMember]
        public string Room { get; set; }

        /// <summary>
        /// Hotels Room Price
        /// </summary>
        [DataMember]
        public double Price { get; set; }
    }
}
