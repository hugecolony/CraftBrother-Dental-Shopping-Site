using System.Text.Json;

namespace CraftBrothers.Utility
{
    public static class SessionExtension
    {
        //setting session
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        //getting session
        public static T Get<T>(this ISession session, string key)
        {

            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
            //session.SetString(key, JsonSerializer.Serialize(value));
        }
    }
}
