using Spectre.Console.Cli;
using SwAddinManagerCli.Commands;

namespace SwAddinManagerCli;

internal class Program
{
    static int Main(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            config.AddCommand<ListCommand>("list");

            config.AddCommand<DisableAllCommand>("disable");
            config.AddCommand<DisableAllCommand>("enable");

            config.AddCommand<DisableAllCommand>("enableall");
            config.AddCommand<DisableAllCommand>("disableall");

            // config.SetHelpProvider<SwAddinManagerHelpProvider>();
        });

        return app.Run(args);
    }
}
