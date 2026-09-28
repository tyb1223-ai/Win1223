using System.Threading;

namespace Win1223;

internal static class Program
{
    [STAThread]
    static async Task Main()
    {
        using var mutex =
            new Mutex(true, "Win1223Singleton", out bool first);

        if (!first)
            return;

        ApplicationConfiguration.Initialize();

        var splash = new SplashForm();

        splash.Show();

        splash.SetStatus("正在唤醒电脑...", 10);

        var ok = await WakeService.WakeAsync();

        if (!ok)
        {
            MessageBox.Show(
                "唤醒失败。",
                "Win1223");

            return;
        }

        splash.SetStatus("等待远程桌面...", 30);

        ok = await RdpLauncher.WaitForPortAsync();

        if (!ok)
        {
            MessageBox.Show(
                "等待远程桌面超时。",
                "Win1223");

            return;
        }

        splash.SetStatus("正在打开客户端...", 100);

        await Task.Delay(200);

        splash.Hide();

        RdpLauncher.Launch();
    }
}
