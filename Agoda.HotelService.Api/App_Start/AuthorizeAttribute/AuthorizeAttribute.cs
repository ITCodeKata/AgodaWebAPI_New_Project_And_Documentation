namespace Agoda.HotelService.Api.App_Start.AuthorizeAttribute
{
    using System.Web;

    /// <summary>
    /// Verifies the request
    /// </summary>
    public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {

        /// <summary>
        /// Specifies the Authorization filter
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}