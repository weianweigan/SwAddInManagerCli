using Spectre.Console;

namespace SwAddinManagerCli.Utils;

internal static class Converter
{
    public static Table ToTable(List<AddIn> addIns)
    {
        var addinTable = new Table();
        addinTable.AddColumns("Id", "Title", "Description", "Startup");
        foreach (var addIn in addIns)
        {
            addinTable.AddRow(
                addIn.AddInID.ToString(),
                addIn.Title!,
                addIn.Description!,
                addIn.Startup?.ToString() ?? ""
            );
        }

        return addinTable;
    }
}
