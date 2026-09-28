using System.Threading;

namespace Win1223;

internal static class Program
{
    [STAThread]
    static async Task Main()
    {
        using var mutex = new Mutex(true, "Win1223Singleton", out bool first);

        if (!first)
            return;

        bool ok = await WakeService.WakeAsync();

        if (!ok)
        {
            NativeMessageBox.Show(
                "唤醒服务器失败。",
                "Win1223");

            return;
        }

        ok = await RdpLauncher.WaitForPortAsync();

        if (!ok)
        {
            NativeMessageBox.Show(
                "等待 Windows 虚拟机启动超时（60 秒）。",
                "Win1223");

            return;
        }

        RdpLauncher.Launch();
    }
}
