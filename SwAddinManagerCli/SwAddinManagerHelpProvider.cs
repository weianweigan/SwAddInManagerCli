using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Help;
using Spectre.Console.Rendering;

namespace SwAddinManagerCli;

public class SwAddinManagerHelpProvider : HelpProvider
{
    public SwAddinManagerHelpProvider(ICommandAppSettings settings)
        : base(settings)
    {
    }

    public override IEnumerable<IRenderable> GetHeader(ICommandModel model, ICommandInfo? command)
    {
        return
        [
            new Text("--------------------------------------"), Text.NewLine,
            new Text("---       SwAddinManagerHelp       ---"), Text.NewLine,
            new Text("--------------------------------------"), Text.NewLine,
            Text.NewLine,
        ];
    }
}