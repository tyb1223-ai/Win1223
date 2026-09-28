using System.Diagnostics;
using System.Net.Sockets;

namespace Win1223;

public static class RdpLauncher
{
    public static async Task<bool> WaitForPortAsync()
    {
        var end =
            DateTime.Now.AddSeconds(
                Config.MaxWaitSeconds);

        while (DateTime.Now < end)
        {
            using var tcp = new TcpClient();

            try
            {
                var task =
                    tcp.ConnectAsync(
                        Config.Host,
                        Config.Port);

                if (await Task.WhenAny(task, Task.Delay(300)) == task &&
                    tcp.Connected)
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
        Process.Start(new ProcessStartInfo
        {
            FileName = "mstsc.exe",
            Arguments = $"/v:{Config.Host}:{Config.Port}",
            UseShellExecute = true
        });
    }
}
