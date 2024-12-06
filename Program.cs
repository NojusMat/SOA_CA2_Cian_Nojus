using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SOA_CA2_Cian_Nojus.Authentication;
using SOA_CA2_Cian_Nojus.Data;
using DotNetEnv;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Configure rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedPolicy", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);    // Time window of 1 minute
        opt.PermitLimit = 100;                   // Allow 100 requests per minute
        opt.QueueLimit = 2;                      // Queue limit of 2
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddDbContext<SOA_CA2_Cian_NojusContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SOA_CA2_Cian_NojusContext") ?? throw new InvalidOperationException("Connection string 'SOA_CA2_Cian_NojusContext' not found.")));

// Add services to the container.
DotNetEnv.Env.Load();



// The code used to allow CORS was taken from the following link: https://www.c-sharpcorner.com/article/cross-origin-resource-sharing-cors-in-net-8/#:~:text=Cross-Origin%20Resource%20Sharing%20%28CORS%29%20in%20.NET%208%201,Common%20Issues%20and%20Troubleshooting%20...%208%20Conclusion%20
builder.Services.AddCors(options =>
{
	options.AddPolicy("CustomCORS",
		builder => builder.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader());
});


builder.Services.AddControllers(/*x => x.Filters.Add<ApiKeyAuthFilter>()*/);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
})
    .AddMvc();
builder.Services.AddSwaggerGen( c =>
{
    // Add a security definition for the ApiKey scheme
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Api key to access the api",
        Type = SecuritySchemeType.ApiKey,
        Name = "x-api-key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"

    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        {scheme, new List<string>()}
    };
    c.AddSecurityRequirement(requirement);
});

// Add the API key authentication filter
builder.Services.AddScoped<ApiKeyAuthFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomCORS");

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
