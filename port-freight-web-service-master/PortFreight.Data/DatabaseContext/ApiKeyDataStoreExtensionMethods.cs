using Google.Cloud.Datastore.V1;
using PortFreight.Data.Entities;

namespace PortFreight.Data;

public static class ApiKeyDataStoreExtensionMethods
{

    public static Key ToKey(this string id) =>
        new Key().WithElement("ApiKey", id);


    public static Entity ToEntity(this ApiKey apiKey) => new Entity()
    {
        ["Id"] = apiKey.Id,
        ["Token"] = apiKey.Token,
        ["Source"] = apiKey.Source
    };

    public static ApiKey ToApiKey(this Entity entity) => new ApiKey()
    {
        Id = (string)entity["Id"],
        Token = (string)entity["Token"]
    };
}
