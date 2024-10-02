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

            config.AddCommand<DisableCommand>("disable");
            config.AddCommand<EnableCommand>("enable");

            config.AddCommand<EnableAllCommand>("enableall");
            config.AddCommand<DisableAllCommand>("disableall");
        });

        return app.Run(args);
    }
}
