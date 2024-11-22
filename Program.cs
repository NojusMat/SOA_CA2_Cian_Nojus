using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SOA_CA2_Cian_Nojus.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SOA_CA2_Cian_NojusContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SOA_CA2_Cian_NojusContext") ?? throw new InvalidOperationException("Connection string 'SOA_CA2_Cian_NojusContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
