using Spectre.Console;
using Spectre.Console.Cli;
using SwAddinManagerCli.Utils;
using System.ComponentModel;

namespace SwAddinManagerCli.Commands;

public class EnableCommand : Command<EnableCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        if (string.IsNullOrEmpty(settings.AddInName))
        {
            AnsiConsole.MarkupLine($"[red]Please input addIn name that you want to enable![/]");
            return 1;
        }

        List<AddIn> addInsList = AddInUtils.ListAddIns().ToList();
        Dictionary<string, AddIn> addInsDictionary = addInsList.ToDictionary(
            p => p.AddInID.ToString(),
            p => p
        );
        string[]? enableAddInNames = settings.AddInName?.Split(',', '，');
        List<string> enableAddInIds = [];
        if (enableAddInNames != null)
        {
            foreach (var name in enableAddInNames)
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
                    enableAddInIds.Add(id);
                }
            }
        }

        AddInUtils.EnableAddInsStartup(enableAddInIds);
        if (settings.Only == true)
        { 
            AddInUtils.DisableAllAddInsStartup(out _, [.. enableAddInIds]);
        };

        return 0;
    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[AddInName]")]
        [Description("AddIn names that you want to load when solidworks opened, Split by ,")]
        public string? AddInName { get; set; }

        [Description("Option that make Solidworks only load addins which you spec. Other addIns will be disabled")]
        [CommandOption("--only")]
        public bool? Only { get; set; }
    }
}