using EC_User.FunctionApp.Controllers;
using EC_User.FunctionApp.Services;
using EC_User.Domain.Repositories;
using EC_User.Infrastructure.Contexts;
using EC_User.Infrastructure.Repositories;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EC_User.FunctionApp.Middlewares;
using System.IdentityModel.Tokens.Jwt;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Configuration.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
var connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<EC_UserContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddGraphQLFunction()
    .AddQueryType<EC_UserGraphQLController>()
    .AddMutationType<EC_UserMutationGraphQLController>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<EC_UserGraphQLController>();
builder.Services.AddScoped<EC_UserMutationGraphQLController>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<CharacterService>();

builder.Services.AddScoped<GraphQLHttpClient>();

var jwtSettings = new JwtSettings()
{
    SecretKey = builder.Configuration.GetSection("JwtSettings")
            .GetValue<string>("SecretKey")!
};
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthenticationMiddleware>();
builder.Services.AddTransient<AuthenticationMiddleware>();
builder.Services.AddScoped<UserContext>();

builder.Services.AddAuthorization();

builder.UseMiddleware<AuthenticationMiddleware>();

builder.Build().Run();