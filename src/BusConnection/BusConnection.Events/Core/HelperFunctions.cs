using RabbitMQ.Client;

namespace BusConnections.Events.Core
{
    public static class HelperFunctions
    {
        public static int GetRetryCount(IBasicProperties messageProperties, string countHeader)
        {
            IDictionary<string, object> headers = messageProperties.Headers;
            var count = 0;
            if (headers == null) return count;
            if (!headers.ContainsKey(countHeader)) return count;
            var countAsString = Convert.ToString(headers[countHeader]);
            count = Convert.ToInt32(countAsString);
            return count;
        }

        public static IDictionary<string, object> CopyHeaders(IBasicProperties originalProperties)
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();
            IDictionary<string, object> headers = originalProperties.Headers;
            if (headers == null) return dict;
            foreach (var kvp in headers)
            {
                dict[kvp.Key] = kvp.Value;
            }
            return dict;
        }
    }
}
