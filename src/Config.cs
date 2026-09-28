using System;

namespace Win1223;

public static class Config
{
    public const string ApiUrl = "https://win.1223.fun:8888/api/wake";
    public const string ApiToken = "3141592653589793238462643383279";

    public const string Host = "win.1223.fun";
    public const int Port = 3400;
    public const int MaxWaitSeconds = 60;

    // Logger.cs 用到的日志目录
    public static readonly string LogDir =
        System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Win1223",
            "Logs");
}
