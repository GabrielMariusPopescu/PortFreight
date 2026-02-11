namespace Notify.Models.Responses;

public class LetterNotificationResponse : NotificationResponse
{
    public LetterResponseContent content;
    public string postage;

    public override bool Equals(object response)
    {
            i
  
    (
    !
    (respons
     i
     LetterNotificationRespons
     res
    p
    ))
            {
                retur
     fals
    e;
            } retur
  
    (conten
     =
     res
    p
    .conten
     |
     conten
    t
    .Equal
    s
    (res
    p
    .conten
    t
    )
     &
     bas
    e
    .Equal
    s
    (res
    p
     &
  
    (postag
     =
     res
    p
    .postag
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

public class LetterResponseContent
{
    public string body;
    public string subject;

    public override bool Equals(object other)
    {
            i
  
    (
    !
    (othe
     i
     LetterResponseConten
  
    o
    ))
            {
                retur
     fals
    e;
            } retur
     bod
     =
  
    o
    .bod
     &
     subjec
     =
  
    o
    .subjec
    t;
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