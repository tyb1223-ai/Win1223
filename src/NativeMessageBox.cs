using System.Runtime.InteropServices;

namespace Win1223;

public static class NativeMessageBox
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(
        IntPtr hWnd,
        string text,
        string caption,
        uint type);

    public static void Show(string text, string caption)
    {
        MessageBoxW(IntPtr.Zero, text, caption, 0x40);
    }
}
