using System.Runtime.InteropServices;

namespace Win1223;

public partial class SplashForm : Form
{
    public SplashForm()
    {
        InitializeComponent();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        // Windows 11 原生圆角
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000))
        {
            const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
            const int DWMWCP_ROUND = 2;

            int preference = DWMWCP_ROUND;

            DwmSetWindowAttribute(
                Handle,
                DWMWA_WINDOW_CORNER_PREFERENCE,
                ref preference,
                sizeof(int));
        }
    }

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(
        IntPtr hwnd,
        int attribute,
        ref int value,
        int size);

    public void SetStatus(string text)
    {
        if (InvokeRequired)
        {
            Invoke(() => SetStatus(text));
            return;
        }

        lblStatus.Text = text;
    }

    public void SetProgress(int value)
    {
        if (InvokeRequired)
        {
            Invoke(() => SetProgress(value));
            return;
        }

        progress.Value = Math.Max(0, Math.Min(100, value));
    }
}
