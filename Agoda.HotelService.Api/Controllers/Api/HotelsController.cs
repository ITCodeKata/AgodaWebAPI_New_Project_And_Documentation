namespace Agoda.HotelService.Api.Controllers.Api
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web.Http;
    using System.Diagnostics;
    using Agoda.HotelService.Common;
    using Agoda.HotelService.Entities.HotelsModel;
    using Agoda.HotelService.Business.HotelsProvider;
    using static Agoda.HotelService.Common.CommonEnum;

    /// <summary>
    /// Hotels Api Controller
    /// </summary>
    [RoutePrefix("hotels/api")]
    public class HotelsController : BaseApiController
    {
        public HotelsController()
        {
            Debug.WriteLine("Hello" + _identity.Name + " Role:" + string.Join(",", _roles.ToList()));
        }

        /// <summary>
        /// Authorization: AllowAnonymous role
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IHttpActionResult</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("getbycityid")]
        public IHttpActionResult GetByCityId(HotelsRequestModel model)
        {
            if(_isRateLimitApplied)
                return Content(HttpStatusCode.MethodNotAllowed, ApplicationConstant.RATE_LIMIT_APPLIED);

            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, ModelState);

            HotelsResponse response = new HotelsResponse();
            try
            {
                var res = HotelsProvider.GetHotelsByCityId(model, out int totalRecordCount);
                response.Response = res;
                response.TotalRecordCount = totalRecordCount;

                if (totalRecordCount != 0)
                {
                    response.Successful = true;
                    response.ResponseCode = ResponseCode.Success;
                }
                else
                {
                    response.Successful = false;
                    response.ResponseCode = ResponseCode.Fail;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Log exception in db
                Debug.WriteLine(string.Format("Error {0}", ex.Message));
                return Content(HttpStatusCode.InternalServerError, ModelState);
            }
        }

        /// <summary>
        /// Authenticate: Authenticate
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IHttpActionResult</returns>
        [HttpPost]
        [Authorize]
        [Route("getbycityid2")]
        public IHttpActionResult GetByCityId_Authenticate(HotelsRequestModel model)
        {
            if (_isRateLimitApplied)
                return Content(HttpStatusCode.MethodNotAllowed, ApplicationConstant.RATE_LIMIT_APPLIED);

            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, ModelState);

            HotelsResponse response = new HotelsResponse();
            try
            {
                var res = HotelsProvider.GetHotelsByCityId(model, out int totalRecordCount);
                response.Response = res;
                response.TotalRecordCount = totalRecordCount;

                if (totalRecordCount != 0)
                {
                    response.Successful = true;
                    response.ResponseCode = ResponseCode.Success;
                }
                else
                {
                    response.Successful = false;
                    response.ResponseCode = ResponseCode.Fail;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Log exception in db
                Debug.WriteLine(string.Format("Error {0}", ex.Message));
                return Content(HttpStatusCode.InternalServerError, ModelState);
            }
        }

        /// <summary>
        /// Authorization: Authorize Role allows
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IHttpActionResult</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("getbycityid3")]
        public IHttpActionResult GetByCityId_Authorize(HotelsRequestModel model)
        {
            if (_isRateLimitApplied)
                return Content(HttpStatusCode.MethodNotAllowed, ApplicationConstant.RATE_LIMIT_APPLIED);

            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, ModelState);

            HotelsResponse response = new HotelsResponse();
            try
            {
                var res = HotelsProvider.GetHotelsByCityId(model, out int totalRecordCount);
                response.Response = res;
                response.TotalRecordCount = totalRecordCount;

                if (totalRecordCount != 0)
                {
                    response.Successful = true;
                    response.ResponseCode = ResponseCode.Success;
                }
                else
                {
                    response.Successful = false;
                    response.ResponseCode = ResponseCode.Fail;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Log exception in db
                Debug.WriteLine(string.Format("Error {0}", ex.Message));
                return Content(HttpStatusCode.InternalServerError, ModelState);
            }
        }
    }
}
