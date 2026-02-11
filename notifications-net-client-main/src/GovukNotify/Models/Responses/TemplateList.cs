using System.Collections.Generic;
using Newtonsoft.Json;

namespace Notify.Models.Responses;

public class TemplateList
{
    [JsonProperty("templates")] public List<TemplateResponse> templates;
}