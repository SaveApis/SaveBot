using System.Reflection;
using Bot.Application.Backend.GraphQL;
using SaveApis.Core.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WithAssemblies(Assembly.GetExecutingAssembly()).WithAutofac<BotQuery, BotMutation>();

builder.Services.AddControllers();

var app = builder.Build();

await app.RunSaveApisAsync();