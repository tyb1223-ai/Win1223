using System.Runtime.InteropServices;

namespace Win1223;

public static partial class NativeMessageBox
{
    [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int MessageBoxW(
        nint hWnd,
        string text,
        string caption,
        uint type);

    public static void Show(
        string text,
        string caption)
    {
        MessageBoxW(
            0,
            text,
            caption,
            0x00000040);
    }
}
