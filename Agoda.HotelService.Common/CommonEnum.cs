namespace Agoda.HotelService.Common
{
    /// <summary>
    /// Enumeration For The System
    /// </summary>
    public class CommonEnum
    {
        #region Enum

        /// <summary>
        /// Sort Direction
        /// </summary>
        public enum SortDirection
        {
            Asc,
            Desc
        }
        #endregion

        #region Response Code
        /// <summary>
        /// Enumeration For Error Code
        /// </summary>
        public enum ResponseCode
        {
            /// <summary>
            /// Enumeration For Success
            /// </summary>
            Success,
            /// <summary>
            /// Enumeration For Fail
            /// </summary>
            Fail
        }
        #endregion
    }
}
