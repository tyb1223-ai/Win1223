using System.Net.Http;
using System.Net.Http.Headers;

namespace Win1223;

public static class WakeService
{
    private static readonly HttpClient client = new();

    public static async Task<bool> WakeAsync()
    {
        try
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Config.ApiToken);

            using var response =
                await client.PostAsync(
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
