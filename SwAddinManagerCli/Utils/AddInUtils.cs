using Microsoft.Win32;

namespace SwAddinManagerCli.Utils;

public class AddIn(string? title, string? description, Guid addInID, bool? startup = false)
{
    public string? Title { get; } = title;

    public string? Description { get; } = description;

    public Guid AddInID { get; } = addInID;

    public bool? Startup { get; } = startup;

    public override string ToString()
    {
        return $"AddInId:{AddInID}, Title:{Title}, Description:{Description}";
    }
}

internal static class AddInUtils
{
    private const string AddInStartupPath = "Software\\SolidWorks\\AddInsStartup";

    public static IEnumerable<AddIn> ListAddIns()
    {
        RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(
            @"SOFTWARE\SolidWorks\AddIns",
            writable: false
        );

        if (registryKey == null)
        {
            yield break;
        }

        string[] subKeyNames = registryKey.GetSubKeyNames();

        if (subKeyNames == null)
        {
            yield break;
        }

        var startupStates = GetAddInsStartup();
        foreach (var subKeyName in subKeyNames)
        {
            RegistryKey? subRegistryKey = registryKey.OpenSubKey(subKeyName, writable: false);
            if (subRegistryKey == null)
            {
                continue;
            }

            string key = Path.GetFileName(subRegistryKey.Name);
            Guid id = Guid.Parse(key);
            string? title = subRegistryKey.GetValue("Title")?.ToString();
            string? description = subRegistryKey.GetValue("Description")?.ToString();
            bool? state = null;
            if (startupStates.TryGetValue(key, out var startupState))
            {
                state = startupState;
            }

            yield return new AddIn(title, description, id, state);
        }
    }

    public static Dictionary<string, bool> GetAddInsStartup()
    {
        Dictionary<string, bool> keys = [];
        RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(
            AddInStartupPath,
            writable: true
        );
        if (registryKey == null)
        {
            return keys;
        }
        string[] subKeyNames = registryKey.GetSubKeyNames();
        if (subKeyNames == null)
        {
            return keys;
        }
        string[] array = subKeyNames;
        foreach (string text in array)
        {
            RegistryKey? subRegistryKey = registryKey.OpenSubKey(text, writable: true);
            if (subRegistryKey == null)
            {
                continue;
            }
            if (int.TryParse(subRegistryKey.GetValue("")?.ToString(), out var result))
            {
                keys.Add(Path.GetFileName(subRegistryKey.Name), result == 1);
            }
        }

        return keys;
    }

    public static void DisableAllAddInsStartup(
        out IReadOnlyList<string> disabledAddInGuids,
        string[]? skipAddins = null
    )
    {
        List<string> list = [];
        RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(
            AddInStartupPath,
            writable: true
        );
        if (registryKey == null)
        {
            disabledAddInGuids = [];
            return;
        }
        string[] subKeyNames = registryKey.GetSubKeyNames();
        if (subKeyNames == null)
        {
            disabledAddInGuids = [];
            return;
        }
        string[] array = subKeyNames;
        foreach (string text in array)
        {
            RegistryKey? subRegistryKey = registryKey.OpenSubKey(text, writable: true);
            if (subRegistryKey == null)
            {
                continue;
            }
            if (
                int.TryParse(subRegistryKey.GetValue("")?.ToString(), out var result)
                && result == 1
            )
            {
                subRegistryKey.SetValue("", 0);
                list.Add(text);
            }
        }

        disabledAddInGuids = list;
    }

    public static void DisableAddInsStartup(IReadOnlyList<string> addInIds)
    {
        List<string> list = [];
        RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(
            AddInStartupPath,
            writable: true
        );
        if (registryKey == null)
        {
            return;
        }
        string[] subKeyNames = registryKey.GetSubKeyNames();
        if (subKeyNames == null)
        {
            return;
        }
        string[] array = subKeyNames;
        foreach (string text in array)
        {
            RegistryKey? subRegistryKey = registryKey.OpenSubKey(text, writable: true);
            if (subRegistryKey == null)
            {
                continue;
            }

            string id = Path.GetFileName(subRegistryKey.Name);
            if (!addInIds.Contains(id))
            {
                continue;
            }

            if (
                int.TryParse(subRegistryKey.GetValue("")?.ToString(), out var result)
                && result == 1
            )
            {
                subRegistryKey.SetValue("", 0);
                list.Add(text);
            }
        }
    }

    public static void EnableAddInsStartup(IReadOnlyList<string> addInGuids)
    {
        RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(
            AddInStartupPath,
            writable: true
        );
        foreach (string addInGuid in addInGuids)
        {
            registryKey?.OpenSubKey(addInGuid, writable: true)?.SetValue("", 1);
        }
    }
}
