using System.Collections.Generic;
using Newtonsoft.Json;

namespace Notify.Models.Responses;

public class NotifyHTTPErrorResponse
{
#pragma warning disable 169, 649
    [JsonProperty("status_code")] private string statusCode;

    [JsonProperty("errors")] private List<NotifyHTTPError> errors;

    public string getStatusCode()
    {
        return statusCode;
    }

    public string getErrorsAsJson()
    {
        return JsonConvert.SerializeObject(errors, Formatting.Indented);
    }
}