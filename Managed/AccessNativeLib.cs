using System.Runtime.InteropServices;
namespace SharedDllTest;
public class AccessNativeLib
{
    [DllImport("Hello")]
    public static extern void globalHello();

    [DllImport("Hello")]
    public static extern void namespaceHello();

    public static void SayHello()
    {
        globalHello();
        namespaceHello();
    }
}
