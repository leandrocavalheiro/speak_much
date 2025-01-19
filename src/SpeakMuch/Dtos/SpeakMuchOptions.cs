namespace SpeakMuch.Dtos;

public class SpeakMuchOptions
{
    public string ResourcesPath { get; set; } = string.Empty;
    public string ResourceName { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
    public bool UseMultiplesFiles { get; set; }
    
    public SpeakMuchOptions(){}
    public SpeakMuchOptions(
        string resourcesPath,
        string resourceName,
        string languages,
        string language,
        string context = "",
        bool useMultiplesFiles = false)
    {
        ResourcesPath = resourcesPath;
        ResourceName = resourceName;
        Languages = languages;
        Language = language;
        Context = context;
        UseMultiplesFiles = useMultiplesFiles;
    }    
    
    public SpeakMuchOptions WithResourcePath(string resourcePath)
    {
        ResourcesPath = resourcePath;
        return this;
    }

    public SpeakMuchOptions WithResourceName(string resourceName)
    {
        ResourceName = resourceName;
        return this;
    }

    public SpeakMuchOptions WithSupportedLanguages(string languages)
    {
        Languages = languages;
        return this;
    }

    public SpeakMuchOptions WithLanguage(string language)
    {
        Language = language;
        return this;
    }

    public SpeakMuchOptions WithContext(string context)
    {
        Context = context;
        return this;
    }

    public SpeakMuchOptions WithUseMultiplesFiles(bool useMultiplesFiles)
    {
        UseMultiplesFiles = useMultiplesFiles;
        return this;
    }    
}