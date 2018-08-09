namespace Agoda.HotelService.Api.Controllers.Api
{
    using System.Web;
    using System.Linq;
    using System.Web.Http;
    using System.Diagnostics;
    using System.Security.Claims;
    using Agoda.HotelService.Api.App_Start.RateLimiter;
    using Agoda.HotelService.Common;

    /// <summary>
    /// BaseApiController
    /// </summary>
    public class BaseApiController : ApiController
    {
        public readonly string _roles = string.Empty;
        public bool _isRateLimitApplied = default(bool);
        public readonly ClaimsIdentity _identity = null;

        /// <summary>
        /// Constructor BaseApiController
        /// Check for IsRateLimitApplied {bool}
        /// </summary>
        public BaseApiController()
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    _identity = (ClaimsIdentity)User.Identity;

                    var key = _identity.Claims.Where(c => c.Type == ClaimTypes.SerialNumber)
                                .Select(c => c.Value)
                                .SingleOrDefault();

                    var bucketSize = _identity.Claims.Where(c => c.Type == ClaimTypes.UserData)
                                    .Select(c => c.Value)
                                    .SingleOrDefault();

                    var roles = _identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                                .Select(c => c.Value);

                    if (!string.IsNullOrEmpty(key))
                    {
                        bucketSize = string.IsNullOrEmpty(bucketSize) ? ApplicationConstant.MAX_BUCKET_SIZE.ToString() : bucketSize;
                        double.TryParse(bucketSize, out double outVal);

                        /*
                         * Ppposite applied because ApplyRateLimit returns _consume(bool)
                         */
                        _isRateLimitApplied = !(new RateLimiter().ApplyRateLimit(key, outVal));
                    }

                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(string.Format("Error {0}", ex.Message));
            }

        }
    }

}
