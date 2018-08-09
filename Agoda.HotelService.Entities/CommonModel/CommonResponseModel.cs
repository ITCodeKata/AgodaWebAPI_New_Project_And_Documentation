namespace Agoda.HotelService.Entities.CommonModel
{
    using Agoda.HotelService.Common;
    using System.Runtime.Serialization;

    #region [Error Response] 

    /// <summary>
    /// Error Response Model
    /// </summary>
    [DataContract]
    public class CommonResponseModel
    {
        /// <summary>
        /// Response Status
        /// </summary>
        [DataMember]
        public bool Successful { get; set; }
        /// <summary>
        /// Error Message
        /// </summary>
        [DataMember]
        public string ResponseMessage { get; set; }
        /// <summary>
        /// Error Code
        /// </summary>
        [DataMember]
        public CommonEnum.ResponseCode ResponseCode { get; set; }
    }
    #endregion
}
