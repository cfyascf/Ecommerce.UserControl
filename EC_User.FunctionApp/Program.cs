using EC_User.AppFunction.Controllers;
using EC_User.AppFunction.Services;
using EC_User.Domain.Repositories;
using EC_User.Infrastructure.Contexts;
using EC_User.Infrastructure.Repositories;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

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

builder.Build().Run();