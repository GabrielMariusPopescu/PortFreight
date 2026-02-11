using Newtonsoft.Json;

namespace Notify.Models.Responses;

public class EmailNotificationResponse : NotificationResponse
{
    public EmailResponseContent content;

    public override bool Equals(object response)
    {
        if (!(response is EmailNotificationResponse resp)) return false;

        return
            content.fromEmail == resp.content.fromEmail &&
            content.body == resp.content.body &&
            content.subject == resp.content.subject &&
            content.oneClickUnsubscribeURL == resp.content.oneClickUnsubscribeURL &&
            base.Equals(resp);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public class EmailResponseContent
{
    public string body;

    [JsonProperty("from_email")] public string fromEmail;

    [JsonProperty("one_click_unsubscribe_url")]
    public string oneClickUnsubscribeURL;

    public string subject;
}