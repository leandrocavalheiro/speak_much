using System.Text.Json;
using static SpeakMuch.Constants.SpeakMuchConstants;

namespace SpeakMuch.Extensions;

public static class StringExtensions
{
    public static TResult GetFromEnvironmentVariable<TResult>(this string value, TResult defaultValue = default)
    {
        var result = Environment.GetEnvironmentVariable(value);
        if (string.IsNullOrWhiteSpace(result))
            return defaultValue;
        
        var targetType = typeof(TResult);
        
        if (targetType == typeof(bool))
            return (TResult)(object)bool.Parse(result);
            
        if (targetType == typeof(int))
            return (TResult)(object)int.Parse(result);
        
        if (targetType == typeof(double))
            return (TResult)(object)double.Parse(result);
        
        if (targetType == typeof(decimal))
            return (TResult)(object)decimal.Parse(result);
        
        if (targetType == typeof(string))
            return (TResult)(object)result;
        
        return (TResult)Convert.ChangeType(result, targetType);
    }
    public static TResponse Deserialize<TResponse>(this string value)
    {
        try
        {
            return JsonSerializer.Deserialize<TResponse>(value, JsonConfigFormatted);
        }
        catch (Exception)
        {
            return default;
        }       
    }
}