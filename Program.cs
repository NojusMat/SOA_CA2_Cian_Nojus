using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SOA_CA2_Cian_Nojus.Authentication;
using SOA_CA2_Cian_Nojus.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SOA_CA2_Cian_NojusContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SOA_CA2_Cian_NojusContext") ?? throw new InvalidOperationException("Connection string 'SOA_CA2_Cian_NojusContext' not found.")));

// Add services to the container.


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

builder.Services.AddSwaggerGen( c =>
{
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


app.UseAuthorization();

app.MapControllers();

app.Run();
