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

        public async Task Invoke(HttpContext context)
        {
            string extractedApiKey = null;

            if (context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var headerApiKey))
                {
                extractedApiKey = headerApiKey;
                }
            else if (context.Request.Query.TryGetValue(AuthConstants.ApiKeyHeaderName, out var queryApiKey))
            {
                extractedApiKey = queryApiKey;
            }
            if(string.IsNullOrEmpty(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key was not provided");
                return;
            }
            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
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