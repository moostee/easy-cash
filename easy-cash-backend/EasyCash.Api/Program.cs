using EasyCash.Middlewares;
using EasyCash.Application;
using EasyCash.Api;
using EasyCash.Infrastructure;
using EasyCash.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables("EasyCash_");

// Add services to the container.
builder.Services.AddCors();

builder.Services
            .AddApplication(builder.Configuration)
            .AddInfrastructure(builder.Configuration)
            .ConfigureAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwagger();

var app = builder.Build();//.RunMigration<EasyCashContext>();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// app.UseInfrastructure();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
