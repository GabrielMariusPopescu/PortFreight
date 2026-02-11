using Newtonsoft.Json;

namespace Notify.Models.Responses;

public class SmsNotificationResponse : NotificationResponse
{
    public SmsResponseContent content;

    public override bool Equals(object response)
    {
            i
  
    (
    !
    (respons
     i
     SmsNotificationRespons
     res
    p
    ))
            {
                retur
     fals
    e;
            } return
                conten
    t
    .bod
     =
     res
    p
    .conten
    t
    .bod
     &&
                conten
    t
    .fromNumbe
     =
     res
    p
    .conten
    t
    .fromNumbe
     &&
                bas
    e
    .Equal
    s
    (respons
    e
    );
        }

    public override int GetHashCode()
    {
            retur
     bas
    e
    .GetHashCod
    e
    (
    );
        }
}

public class SmsResponseContent
{
    public string body;

    [JsonProperty("from_number")] public string fromNumber;
}