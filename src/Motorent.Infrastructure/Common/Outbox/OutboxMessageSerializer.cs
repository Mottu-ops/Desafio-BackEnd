using Newtonsoft.Json;

namespace Motorent.Infrastructure.Common.Outbox;

internal static class OutboxMessageSerializer
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public static string Serialize<T>(T data) where T : class => 
        JsonConvert.SerializeObject(data, SerializerSettings);

    public static T? Deserialize<T>(string data) where T : class => 
        JsonConvert.DeserializeObject<T>(data, SerializerSettings);
}