using Spectre.Console;
using Spectre.Console.Cli;
using SwAddinManagerCli.Utils;

namespace SwAddinManagerCli.Commands;

public class DisableAllCommand : Command<DisableAllCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        List<AddIn> addInsList = AddInUtils.ListAddIns().ToList();
        Dictionary<string, AddIn> addInsDictionary = addInsList.ToDictionary(
            p => p.AddInID.ToString(),
            p => p
        );
        string[]? skipAddInNames = settings.SkipAddIn?.Split(',', '，');
        List<string> skipAddInIds = [];
        if (skipAddInNames != null)
        {
            foreach (var name in skipAddInNames)
            {
                string? id = addInsList
                    .FirstOrDefault(p => p.Title == name || p.AddInID.ToString() == name)
                    ?.AddInID.ToString();
                if (string.IsNullOrWhiteSpace(id))
                {
                    AnsiConsole.WriteLine($"[red]Cannot find addin: {name}[/]");
                }
                else
                {
                    skipAddInIds.Add(id);
                }
            }
        }

        AddInUtils.DisableAllAddInsStartup(
            disabledAddInGuids: out IReadOnlyList<string>? disabledAddIns,
            [.. skipAddInIds]
        );

        if (disabledAddIns.Count == 0)
        {
            AnsiConsole.WriteLine($"No addin disabled!");
        }
        else
        {
            List<AddIn> results = [];
            foreach (var disabledAddIn in disabledAddIns)
            {
                if (addInsDictionary.TryGetValue(disabledAddIn, out AddIn? findedAddIn))
                {
                    results.Add(findedAddIn);
                }
                else
                {
                    results.Add(new AddIn("", "", Guid.Parse(disabledAddIn)));
                }
            }

            var table = Converter.ToTable(results);
            AnsiConsole.Write(table);
        }

        return 0;
    }

    public class Settings : CommandSettings
    {
        [CommandOption("-s|--skip")]
        public string? SkipAddIn { get; set; }
    }
}
