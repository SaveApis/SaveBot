using Discord;
using Discord.WebSocket;
using Hangfire;
using SaveApis.Core.Application.Events;
using SaveApis.Core.Infrastructure.Jobs;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Bot.Application.Jobs.Discord;

public class StartDiscordBotJob(ILogger logger, IConfiguration configuration, DiscordSocketClient client) : BaseJob<ApplicationStartedEvent>(logger)
{
    [JobDisplayName("Start Discord Bot")]
    public override async Task RunAsync(ApplicationStartedEvent @event, CancellationToken cancellationToken = new())
    {
        Log(LogEventLevel.Information, "Start Discord Bot");
        var token = configuration["token"] ?? throw new ArgumentException("TOKEN");

        client.Log += LogAsync;

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
    }

    private Task LogAsync(LogMessage arg)
    {
        var level = arg.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            _ => LogEventLevel.Information
        };
        Log(level, "[{Source}] {Message}", arg.Exception, arg.Source, arg.Message);

        return Task.CompletedTask;
    }
}