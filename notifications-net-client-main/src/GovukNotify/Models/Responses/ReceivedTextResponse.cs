using Newtonsoft.Json;

namespace Notify.Models.Responses;

public class ReceivedTextResponse
{
    public string content;

    [JsonProperty("created_at")] public string createdAt;

    public string id;

    [JsonProperty("notify_number")] public string notifyNumber;

    [JsonProperty("service_id")] public string serviceId;

    [JsonProperty("user_number")] public string userNumber;

    public override bool Equals(object receivedText)
    {
        if (!(receivedText is ReceivedTextResponse text)) return false;

        return
            id == text.id &&
            createdAt == text.createdAt &&
            notifyNumber == text.notifyNumber &&
            userNumber == text.userNumber &&
            serviceId == text.serviceId &&
            content == text.content;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}