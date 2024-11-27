using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SOA_CA2_Cian_Nojus.Authentication
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string extractedApiKey = null;

            if (context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var headerApiKey))
            {

                extractedApiKey = headerApiKey;
            }
            
            else if (context.HttpContext.Request.Query.TryGetValue(AuthConstants.ApiKeyHeaderName, out var queryApiKey))
            {
                extractedApiKey = queryApiKey;
            }
            if (string.IsNullOrEmpty(extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key Missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key is not valid");
                return;
            }

        }
    }
}
