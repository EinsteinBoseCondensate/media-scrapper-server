

using Newtonsoft.Json;

namespace Common.Extensions;
public static class Extensions
{
    public static string? Serialize<T>(this T value, Formatting formatting = Formatting.Indented) => JsonConvert.SerializeObject(value, formatting);
    public static string? GetContent(this Exception exception) => exception.Message ?? exception.InnerException?.Message;
    public static string SafeName(this Type type) => type.FullName ?? type.Name;
}
