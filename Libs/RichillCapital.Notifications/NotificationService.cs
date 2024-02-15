using System.Net.Http.Headers;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Notifications;

public sealed class NotificationService : INotificationService
{
    private const string Url = "https://notify-api.line.me/api/notify";
    private const string HardCodeToken = "rTjS0liSNNJSzAtbvYb5YfdyPUazxszoG65nrf9rtC1";

    public async Task SendLineNotificationAsync(string message)
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HardCodeToken);

        var content = new Dictionary<string, string>
        {
            { "message", "\n" + message }
        };

        var response = await client.PostAsync(Url, new FormUrlEncodedContent(content));
    }
}