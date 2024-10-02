using Spectre.Console;
using Spectre.Console.Cli;
using SwAddinManagerCli.Utils;

namespace SwAddinManagerCli.Commands;

public class ListCommand : Command<ListCommand.Settings>
{
    public class Settings : CommandSettings { }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.MarkupLine("[blue]Solidworks installed:[/]");

        List<SwVersion> solidworks = SolidworksUtils.GetInstalledVersions().ToList();
        if (solidworks.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Solidworks not found in this computer![/]");
            return 0;
        }

        // output
        var table = new Table();
        table.AddColumns("Version", "Path");
        foreach (var sw in solidworks)
        {
            table.AddRow(sw.DisplayName, sw.ExePath.ToString());
        }
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("[blue]AddIns installed:[/]");
        List<AddIn> addIns = AddInUtils.ListAddIns().ToList();
        // output
        Table addinTable = Converter.ToTable(addIns);
        AnsiConsole.Write(addinTable);

        return 0;
    }
}
