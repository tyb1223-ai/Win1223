using System.Threading;

namespace Win1223;

internal static class Program
{
    [STAThread]
    static async Task Main()
    {
        using var mutex =
            new Mutex(
                true,
                "Win1223Singleton",
                out bool first);

        if (!first)
            return;

        ApplicationConfiguration.Initialize();

        using var splash =
            new SplashForm();

        using var cts =
            new CancellationTokenSource();

        splash.FormClosing += (_,__) => cts.Cancel();

        splash.Show();

        splash.SetProgress(10);
        splash.SetStatus("正在连接服务器...");

        bool ok =
            await WakeService.WakeAsync();

        if (!ok)
        {
            MessageBox.Show(
                "无法连接服务器。",
                "Win1223");

            return;
        }

        splash.SetProgress(30);
        splash.SetStatus("正在唤醒云电脑...");

        ok =
            await RdpLauncher.WaitForPortAsync(
                p => splash.SetProgress(p),
                cts.Token);

        if (!ok)
        {
            MessageBox.Show(
                "等待 Windows 启动超时（60 秒）。",
                "Win1223");

            return;
        }

        splash.SetStatus("正在打开远程桌面...");
        splash.SetProgress(100);

        await Task.Delay(250);

        splash.Hide();

        RdpLauncher.Launch();
    }
}
