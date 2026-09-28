using System.Diagnostics;
using System.Net.Sockets;

namespace Win1223;

public static class RdpLauncher
{
    public static async Task<bool> WaitForPortAsync()
    {
        var end = DateTime.Now.AddSeconds(Config.MaxWaitSeconds);

        while (DateTime.Now < end)
        {
            using var tcp = new TcpClient();

            try
            {
                var t = tcp.ConnectAsync(Config.Host, Config.Port);

                var ok = await Task.WhenAny(t, Task.Delay(300));

                if (ok == t && tcp.Connected)
                    return true;
            }
            catch
            {
            }

            await Task.Delay(300);
        }

        return false;
    }

    public static void Launch()
    {
        // 先尝试打开 Windows App
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = "shell:AppsFolder\\MicrosoftCorporationII.Windows365_8wekyb3d8bbwe!Windows365",
                UseShellExecute = true
            });

            return;
        }
        catch
        {
        }

        // 回退到系统自带远程桌面（不会出现未知发布者弹窗）
        Process.Start(new ProcessStartInfo
        {
            FileName = "mstsc.exe",
            Arguments = $"/v:{Config.Host}:{Config.Port}",
            UseShellExecute = true
        });
    }
}
