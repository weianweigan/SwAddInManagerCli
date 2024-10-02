using Microsoft.Win32;

namespace SwAddinManagerCli.Utils;

public enum SwVersion_e
{
    SwPrior2000 = 1,
    Sw2000 = 8,
    Sw2001 = 9,
    Sw2001Plus = 10,
    Sw2003 = 11,
    Sw2004 = 12,
    Sw2005 = 13,
    Sw2006 = 14,
    Sw2007 = 15,
    Sw2008 = 16,
    Sw2009 = 17,
    Sw2010 = 18,
    Sw2011 = 19,
    Sw2012 = 20,
    Sw2013 = 21,
    Sw2014 = 22,
    Sw2015 = 23,
    Sw2016 = 24,
    Sw2017 = 25,
    Sw2018 = 26,
    Sw2019 = 27,
    Sw2020 = 28,
    Sw2021 = 29,
    Sw2022 = 30,
    Sw2023 = 31,
    Sw2024 = 32,
    Sw2025 = 33,
}

internal class SwVersion
{
    public SwVersion_e Major { get; }

    public string ExePath { get; }

    public string DisplayName => "SOLIDWORKS " + Major.ToString().Substring("Sw".Length);

    public Version Version { get; }

    public int ServicePack { get; }

    public int ServicePackRevision { get; }

    internal SwVersion(Version version, string path)
    {
        Version = version;
        Major = (SwVersion_e)version.Major;
        ExePath = path;
    }

    public int CompareTo(SwVersion other)
    {
        if (other is not null)
        {
            int num = Major.CompareTo(other.Major);
            if (num == 0)
            {
                num = ServicePack.CompareTo(other.ServicePack);
                if (num == 0)
                {
                    num = ServicePack.CompareTo(other.ServicePackRevision);
                }
            }

            return num;
        }

        throw new InvalidCastException("Can only compare SOLIDWORKS versions");
    }

    public override int GetHashCode()
    {
        return (int)Major;
    }
}

internal static class SolidworksUtils
{
    public static IEnumerable<SwVersion> GetInstalledVersions()
    {
        foreach (SwVersion_e item in Enum.GetValues(typeof(SwVersion_e)).Cast<SwVersion_e>())
        {
            string name = $"SldWorks.Application.{(int)item}";
            RegistryKey? registryKey = Registry.ClassesRoot.OpenSubKey(name);
            if (registryKey != null)
            {
                bool flag = false;
                string? path = null;
                try
                {
                    path = FindSwPathFromRegKey(registryKey);
                    flag = true;
                }
                catch { }

                if (flag)
                {
                    yield return CreateVersion(item, path);
                }
            }
        }
    }

    public static SwVersion CreateVersion(SwVersion_e vers, string? path)
    {
        return new SwVersion(new Version((int)vers, 0), path!);
    }

    internal static string FindSwPathFromRegKey(RegistryKey swAppRegKey)
    {
        string? classId =
            (
                swAppRegKey.OpenSubKey("CLSID", writable: false)
                ?? throw new NullReferenceException("Incorrect registry value, CLSID is missing")
            )?.GetValue("") as string;
        if (classId == null)
        {
            throw new NullReferenceException("Incorrect registry value, LocalServer32 is missing");
        }

        string? solidworksPath =
            (
                Registry.ClassesRoot.OpenSubKey(
                    "CLSID\\" + classId + "\\LocalServer32",
                    writable: false
                )
                ?? throw new Exception(
                    "Failed to find the class id in the registry. Make sure that application is running as x64 bit process (including 'Prefer 32-bit' option is unchecked in the project settings)"
                )
            )?.GetValue("") as string;
        if (!File.Exists(solidworksPath))
        {
            throw new FileNotFoundException(
                "Path to SOLIDWORKS executable does not exist: " + solidworksPath
            );
        }

        return solidworksPath;
    }
}
