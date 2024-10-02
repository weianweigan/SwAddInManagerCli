using Spectre.Console;
using Spectre.Console.Cli;
using SwAddinManagerCli.Utils;

namespace SwAddinManagerCli.Commands;

public class EnableAllCommand : Command<EnableAllCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        List<AddIn> addInsList = AddInUtils.ListAddIns().ToList();
        string[]? skipAddInNames = settings.SkipAddIn?.Split(',', '，');
        List<string> skipAddInIds = [];
        if (skipAddInNames != null)
        {
            foreach (var name in skipAddInNames)
            {
                var addin = addInsList
                    .FirstOrDefault(p => p.Title == name || p.AddInID.ToString() == name);
                if (addin == null)
                {
                    AnsiConsole.WriteLine($"[red]Cannot find addin: {name}[/]");
                }
                else
                {
                    addInsList.Remove(addin);
                }
            }
        }

        AddInUtils.EnableAddInsStartup(
            addInsList.Select(p => p.AddInID.ToString()).ToList()
        );
        AnsiConsole.WriteLine("[blue]Finished[/]");

        return 0;
    }

    public class Settings : CommandSettings
    {
        [CommandOption("-s|--skip")]
        public string? SkipAddIn { get; set; }
    }
}
