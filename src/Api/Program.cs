using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Shared.Repositories;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddCors();

// add database dependency injection
var connectionString = builder.Configuration.GetConnectionString("Default");
services.AddScoped<IDbConnection>(s => new MySqlConnection(connectionString));
services.AddScoped<IDbTransaction>((s) =>
{
    var conn = s.GetService<IDbConnection>();
    conn.Open();
    return conn.BeginTransaction();
});
services.AddScoped<IJobRepository, JobRepository>();
services.AddScoped<ICompanyRepository, CompanyRepository>();
services.AddScoped<IUserRepository, UserRepository>();

services.AddControllers();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
});

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
