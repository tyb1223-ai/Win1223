using System;
using System.Threading;
using System.Windows.Forms;

namespace Win1223;

internal static class Program
{
    [STAThread]
    static async Task Main()
    {
        using var mutex = new Mutex(true, "Win1223Singleton", out bool first);

        if (!first)
            return;

        ApplicationConfiguration.Initialize();

        bool ok = await WakeService.WakeAsync();

        if (!ok)
        {
            MessageBox.Show("唤醒失败。", "Win1223");
            return;
        }

        ok = await RdpLauncher.WaitForPortAsync();

        if (!ok)
        {
            MessageBox.Show("等待远程桌面超时。", "Win1223");
            return;
        }

        RdpLauncher.Launch();
    }
}
