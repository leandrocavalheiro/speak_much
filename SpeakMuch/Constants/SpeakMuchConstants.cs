using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpeakMuch.Constants;

public static class SpeakMuchConstants
{
    public static readonly JsonSerializerOptions JsonConfigFormatted = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,

    };
    public const string EnvDefaultLanguage = "SPEAK_MUCH_DEFAULT_LANGUAGE";
    public const string EnvResourcesPath = "SPEAK_MUCH_RESOURCES_PATH";
    public const string EnvResourcesFile = "SPEAK_MUCH_RESOURCES_FILE";
    public const string EnvDefaultContext = "SPEAK_MUCH_DEFAULT_CONTEXT";
    public const string EnvUseMultiplesFiles = "SPEAK_MUCH_USE_MULTIPLES_FILES";
    
}