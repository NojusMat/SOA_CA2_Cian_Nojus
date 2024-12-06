namespace SOA_CA2_Cian_Nojus.Authentication
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        // Middleware to check if the API Key is valid
        public async Task Invoke(HttpContext context)
        {
            string extractedApiKey = null;

            // Check if the header contains the API Key
            if (context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var headerApiKey))
                {
                extractedApiKey = headerApiKey;
                }

            // Check if the query contains the API Key
            else if (context.Request.Query.TryGetValue(AuthConstants.ApiKeyHeaderName, out var queryApiKey))
            {
                extractedApiKey = queryApiKey;
            }

            // If the API Key is not provided
            if (string.IsNullOrEmpty(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key was not provided");
                return;
            }
            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

            // If the API Key is not valid
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Api Key is not valid");
                return;
            }

            await _next(context);
        }
    }
}