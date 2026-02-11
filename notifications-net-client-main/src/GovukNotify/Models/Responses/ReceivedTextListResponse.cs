using System.Collections.Generic;
using Newtonsoft.Json;

namespace Notify.Models.Responses;

public class ReceivedTextListResponse
{
    [JsonProperty("links")] public Link links;

    [JsonProperty("received_text_messages")]
    public List<ReceivedTextResponse> receivedTexts;
}