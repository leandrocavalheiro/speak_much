using Microsoft.Extensions.Configuration;

namespace SpeakMuch.Extensions;

public static class ConfigurationsExtensions
{
    public static TResult GetValue<TResult>(this IConfiguration value, string name, TResult defaultValue = default)
    {
        var result = value[name];
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
}