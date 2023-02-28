using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestAPI
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            try
            {
                string path = httpContext.Request.Path.ToString();

                if (!(path.Contains("/authentication") || path.Contains("/signup") || path.Contains("/resetPassword")))
                {
                    if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                    {
                        httpContext.Response.StatusCode = 401;
                        return httpContext.Response.WriteAsync("Access Denied. UnAuthorized access to resource detected.");
                    }

                    string authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
                    string encryptedToken = authorizationHeader.Substring("Bearer ".Length);
                    BusinessLogic.BL objBL = new BusinessLogic.BL();
                    if (objBL.validateoAuthToken(encryptedToken))
                        return _next(httpContext);
                    else
                    {
                        httpContext.Response.StatusCode = 401;
                        return httpContext.Response.WriteAsync("Access Denied. Access Token validation has failed.");
                    }
                }
                else
                    return _next(httpContext);
            }
            catch (Exception ex)
            {
                Common.Logging.fileLog(System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "| " 
                                       + "Exception:" + ex.Message + "|" 
                                       + "Stack Trace:" + ex.StackTrace);
                httpContext.Response.StatusCode = 500;
                return httpContext.Response.WriteAsync("Technical Error. System has failed to respond");
            }
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }    
}