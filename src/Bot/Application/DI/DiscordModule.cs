using Autofac;
using Discord;
using Discord.WebSocket;

namespace Bot.Application.DI;

public class DiscordModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged,
            LogLevel = LogSeverity.Debug,
            AlwaysDownloadUsers = true
        };

        builder.RegisterInstance(config);
        builder.RegisterType<DiscordSocketClient>().AsSelf().SingleInstance();
    }
}