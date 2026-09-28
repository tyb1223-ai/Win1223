using System.Threading;

namespace Win1223;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        using var mutex=new Mutex(true,"Win1223Singleton",out bool first);

        if(!first)
            return;

        ApplicationConfiguration.Initialize();

        var splash=new SplashForm();

        using var cts=new CancellationTokenSource();

        splash.FormClosing+=(_,__)=>cts.Cancel();

        splash.Shown+=(_,__)=>
        {
            Task.Run(async()=>
            {
                splash.SetProgress(10);
                splash.SetStatus("正在连接服务器...");

                if(!await WakeService.WakeAsync())
                {
                    splash.Invoke(()=>MessageBox.Show("无法连接服务器。"));
                    splash.Invoke(()=>splash.Close());
                    return;
                }

                splash.SetProgress(30);
                splash.SetStatus("正在唤醒云电脑...");

                bool ok=await RdpLauncher.WaitForPortAsync(
                    p=>splash.SetProgress(p),
                    cts.Token);

                if(!ok)
                {
                    splash.Invoke(()=>MessageBox.Show("等待 Windows 启动超时。"));
                    splash.Invoke(()=>splash.Close());
                    return;
                }

                splash.SetProgress(100);
                splash.SetStatus("正在打开远程桌面...");

                await Task.Delay(250);

                RdpLauncher.Launch();

                splash.Invoke(()=>splash.Close());
            });
        };

        Application.Run(splash);
    }
}
