using Spectre.Console;
using Spectre.Console.Cli;
using SwAddinManagerCli.Utils;

namespace SwAddinManagerCli.Commands;

public class DisableCommand : Command<DisableCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        if (string.IsNullOrEmpty(settings.AddInName)) 
        {
            AnsiConsole.MarkupLine($"[red]Please input addIn name that you want to disabled![/]");
            return 1;
        }

        List<AddIn> addInsList = AddInUtils.ListAddIns().ToList();
        Dictionary<string, AddIn> addInsDictionary = addInsList.ToDictionary(
            p => p.AddInID.ToString(),
            p => p
        );
        string[]? disableAddInNames = settings.AddInName?.Split(',', '，');
        List<string> disableAddInIds = [];
        if (disableAddInNames != null)
        {
            foreach (var name in disableAddInNames)
            {
                string? id = addInsList
                    .FirstOrDefault(p => p.Title == name || p.AddInID.ToString() == name)
                    ?.AddInID.ToString();
                if (string.IsNullOrWhiteSpace(id))
                {
                    AnsiConsole.MarkupLine($"[red]Cannot find addin: {name}[/]");
                }
                else
                {
                    disableAddInIds.Add(id);
                }
            }
        }

        AddInUtils.DisableAddInsStartup(
            [.. disableAddInIds]
        );
        return 0;
    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[AddInName]")]
        public string? AddInName { get; set; }
    }
}
