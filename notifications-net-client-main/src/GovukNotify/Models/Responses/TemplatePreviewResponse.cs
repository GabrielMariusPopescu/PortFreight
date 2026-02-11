namespace Notify.Models.Responses;

public class TemplatePreviewResponse
{
    public string body;
    public string id;
    public string name;
    public string subject;
    public string type;
    public int version;

    public override bool Equals(object response)
    {
            i
  
    (
    !
    (respons
     i
     TemplatePreviewRespons
     res
    p
    ))
            {
                retur
     fals
    e;
            } return
                i
     =
     res
    p
    .i
     &&
                nam
     =
     res
    p
    .nam
     &&
                typ
     =
     res
    p
    .typ
     &&
                versio
     =
     res
    p
    .versio
     &&
                bod
     =
     res
    p
    .bod
     &&
                subjec
     =
     res
    p
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