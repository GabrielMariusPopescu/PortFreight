using System.Collections.Generic;
using Newtonsoft.Json;
using Notify.Models.Responses;

namespace Notify.Models;

public class NotificationList
{
    [JsonProperty("links")] public Link links;

    [JsonProperty("notifications")] public List<Notification> notifications;
}