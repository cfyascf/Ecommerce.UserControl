using EC_User.AppFunction.Controllers;
using EC_User.AppFunction.Services;
using EC_User.Domain.Repositories;
using EC_User.Infrastructure.Contexts;
using EC_User.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EC_UserContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
    b => b.MigrationsAssembly("EC_User.Infrastructure")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddGraphQLServer()
    .AddQueryType<EC_UserGraphQLController>()
    .AddMutationType<EC_UserMutationGraphQLController>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

builder.Services.AddScoped<EC_UserGraphQLController>();
builder.Services.AddScoped<EC_UserMutationGraphQLController>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.MapGraphQL();

app.UseHttpsRedirection();

app.Run();