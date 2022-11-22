using System.Reflection;
using System.Runtime.InteropServices;
namespace SharedDllTest;
public class AccessNativeLib
{
    static AccessNativeLib()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            ExtractWindowsLibrary("Hello");
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            ExtractLinuxLibrary("Hello");
        }
    }

    [DllImport("Hello")]
    public static extern void globalHello();

    [DllImport("Hello")]
    public static extern void namespaceHello();

    public static void SayHello()
    {
        globalHello();
        namespaceHello();
    }

    static private void ExtractLinuxLibrary(string dllName)
    {
        string assemblyPath = System.Reflection.Assembly.GetAssembly(typeof(AccessNativeLib)).Location;
        string linuxLib = $"lib{dllName}.so";
        
        string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        foreach (string n in names)
        {
            if (n.EndsWith(linuxLib))
            {
                dllName = n;
                break;
            }
        }

        ExtractResource(dllName, Path.Combine(Path.GetDirectoryName(assemblyPath), linuxLib));
    }

    static private void ExtractWindowsLibrary(string dllName)
    {
        bool x64 = IntPtr.Size == 8;

        string dirName = Path.Combine(Path.GetTempPath(), "AccessNativeLib." +
            Assembly.GetExecutingAssembly().GetName().Version.ToString());

        dirName = Path.Combine(dirName, (x64 ? "x64" : "x86"));
        if (!Directory.Exists(dirName))
            Directory.CreateDirectory(dirName);

        dllName = dllName + ".dll";
        string dllPath = Path.Combine(dirName, dllName);

        if (File.Exists(dllPath))
        {
            try
            {
                LoadLibrary(dllPath);
                return;
            }
            catch
            {
            }
        }

        string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        foreach (string n in names)
        {
            if (n.EndsWith(dllName) && n.Contains(x64 ? "x64" : "x86"))
            {
                dllName = n;
                break;
            }
        }

        ExtractResource(dllName, dllPath);

        LoadDll(dllPath);
    }

    private static void ExtractResource(string recourceName, string outputPath)
    {
        using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream(recourceName))
        {
            try
            {
                File.Delete(outputPath);
                using (Stream outFile = File.Create(outputPath))
                {
                    const int sz = 4096;
                    byte[] buf = new byte[sz];
                    while (true)
                    {
                        int nRead = stm.Read(buf, 0, sz);
                        if (nRead < 1)
                            break;
                        outFile.Write(buf, 0, nRead);
                    }
                }
            }
            catch
            {
            }
        }
    }

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
	static extern IntPtr LoadLibrary(string lpFileName);
    private static void LoadDll(string dllPath)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }
        IntPtr h = LoadLibrary(dllPath);
        if (h == IntPtr.Zero)
        {
            throw new Exception("Unable to load library " + dllPath);
        }
    }
}
