using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Win1223
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(5)
            };

            // 唤醒 PVE
            try
            {
                await client.PostAsync("https://win.1223.fun:8888/api/wake", null);
            }
            catch
            {
                // 网络异常继续等待
            }

            // 最多等待25秒，检测RDP端口
            var end = DateTime.Now.AddSeconds(25);

            while (DateTime.Now < end)
            {
                try
                {
                    using var tcp = new TcpClient();
                    var connectTask = tcp.ConnectAsync("win.1223.fun", 3400);

                    var completed = await Task.WhenAny(connectTask, Task.Delay(500));

                    if (completed == connectTask && tcp.Connected)
                        break;
                }
                catch
                {
                }

                await Task.Delay(500);
            }

            // 打开 Windows App
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = "shell:AppsFolder\\MicrosoftCorporationII.Windows365_8wekyb3d8bbwe!Windows365",
                UseShellExecute = true
            });
        }
    }
}
