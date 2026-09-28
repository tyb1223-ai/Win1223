using System.Threading;

namespace Win1223;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        using var mutex = new Mutex(true, "Win1223Singleton", out bool first);

        if (!first)
            return;

        ApplicationConfiguration.Initialize();

        using var cts = new CancellationTokenSource();
        var splash = new SplashForm();

        // 用户关闭窗口时立即取消后台任务
        splash.FormClosing += (_, __) => cts.Cancel();

        // 窗口真正显示后，再启动后台任务
        splash.Shown += (_, __) =>
        {
            Task.Run(async () =>
            {
                try
                {
                    splash.Invoke(() =>
                    {
                        splash.SetProgress(10);
                        splash.SetStatus("正在连接服务器...");
                    });

                    bool ok = await WakeService.WakeAsync();

                    if (!ok)
                    {
                        splash.Invoke(() =>
                        {
                            MessageBox.Show(
                                "无法连接服务器。",
                                "Win1223",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            splash.Close();
                        });

                        return;
                    }

                    splash.Invoke(() =>
                    {
                        splash.SetProgress(30);
                        splash.SetStatus("正在唤醒云电脑...");
                    });

                    ok = await RdpLauncher.WaitForPortAsync(
                        p => splash.Invoke(() => splash.SetProgress(p)),
                        cts.Token);

                    if (!ok)
                    {
                        splash.Invoke(() =>
                        {
                            MessageBox.Show(
                                "等待 Windows 启动超时（60 秒）。",
                                "Win1223",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            splash.Close();
                        });

                        return;
                    }

                    splash.Invoke(() =>
                    {
                        splash.SetProgress(100);
                        splash.SetStatus("正在打开远程桌面...");
                    });

                    await Task.Delay(250, cts.Token);

                    splash.Invoke(() => splash.Hide());

                    RdpLauncher.Launch();

                    splash.Invoke(() => splash.Close());
                }
                catch (OperationCanceledException)
                {
                    // 用户主动关闭窗口，正常退出
                }
                catch (Exception ex)
                {
                    splash.Invoke(() =>
                    {
                        MessageBox.Show(
                            ex.Message,
                            "Win1223",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        splash.Close();
                    });
                }
            });
        };

        Application.Run(splash);
    }
}
