using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SOA_CA2_Cian_Nojus.Authentication
{
    // Api Key Authentication Filter
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


            // Check if the header contains the API Key
            if (context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var headerApiKey))
            {

                extractedApiKey = headerApiKey;
            }
            // Check if the query contains the API Key
            else if (context.HttpContext.Request.Query.TryGetValue(AuthConstants.ApiKeyHeaderName, out var queryApiKey))
            {
                extractedApiKey = queryApiKey;
            }
            if (string.IsNullOrEmpty(extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key Missing");
                return;
            }

            // Check if the API Key is valid
            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key is not valid");
                return;
            }

        }
    }
}
