using System.Net.Http.Headers;

namespace Win1223;

public static class WakeService
{
    private static readonly HttpClient Client =
        new(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        })
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

    public static async Task<bool> WakeAsync()
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Config.ApiToken);

            using var response =
                await Client.PostAsync(
                    Config.ApiUrl,
                    null);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
