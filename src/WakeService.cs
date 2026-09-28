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
                new AuthenticationHeaderValue("Bearer", Config.ApiToken);

            var r = await client.PostAsync(Config.ApiUrl, null);

            return r.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
