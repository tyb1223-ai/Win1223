using System.Diagnostics;
using System.Net.Sockets;

namespace Win1223;

public static class RdpLauncher
{
    public static async Task<bool> WaitForPortAsync(
        Action<int> progress,
        CancellationToken token)
    {
        var start = DateTime.Now;
        var end = start.AddSeconds(Config.MaxWaitSeconds);

        while (DateTime.Now < end)
        {
            token.ThrowIfCancellationRequested();

            using var tcp = new TcpClient();

            try
            {
                var connect =
                    tcp.ConnectAsync(
                        Config.Host,
                        Config.Port);

                if (await Task.WhenAny(connect, Task.Delay(300, token)) == connect &&
                    tcp.Connected)
                {
                    progress(100);
                    return true;
                }
            }
            catch
            {
            }

            var elapsed = DateTime.Now - start;

            var percent =
                30 +
                (int)(elapsed.TotalSeconds /
                      Config.MaxWaitSeconds *
                      65);

            progress(Math.Min(percent, 95));
        }

        return false;
    }

    public static void Launch()
    {
        if (Process.GetProcessesByName("mstsc").Length > 0)
            return;

        Process.Start(new ProcessStartInfo
        {
            FileName = "mstsc.exe",
            Arguments = $"/v:{Config.Host}:{Config.Port}",
            UseShellExecute = true
        });
    }
}
