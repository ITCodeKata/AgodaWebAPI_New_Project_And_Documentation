namespace Agoda.HotelService.Api.App_Start.AuthorizationServer
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// OAuthAuthorization
    /// </summary>
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Validate Client Authentication
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// Grant Resource Owner Credentials
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //go to db
            if (context.UserName == "admin" && context.Password == "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim(ClaimTypes.UserData, "5"));//MaxBucketSize
                identity.AddClaim(new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString()));//User GUID
                identity.AddClaim(new Claim(ClaimTypes.Name, "Admin as: admin"));
                context.Validated(identity);
            }
            else if (context.UserName == "user" && context.Password == "user")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("username", "user"));
                identity.AddClaim(new Claim(ClaimTypes.UserData, "1"));//MaxBucketSize
                identity.AddClaim(new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString()));//User GUID
                identity.AddClaim(new Claim(ClaimTypes.Name, "User as: user"));
                context.Validated(identity);
            }
            else if (context.UserName == "default" && context.Password == "default")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "default"));
                identity.AddClaim(new Claim("username", "default"));
                identity.AddClaim(new Claim(ClaimTypes.UserData, ""));//MaxBucketSize is empty {global MaxBucketSize from config is uesd 5}
                identity.AddClaim(new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString()));//User GUID
                identity.AddClaim(new Claim(ClaimTypes.Name, "User as: default"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "provided username and password is incorrect");
                return;
            }
        }
    }
}