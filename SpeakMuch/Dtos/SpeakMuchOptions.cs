namespace SpeakMuch.Dtos;

public class SpeakMuchOptions
{
    public string ResourcesPath { get; set; }
    public string ResourceName { get; set; }
    public string Languages { get; set; }
    public string Language { get; set; }
    public string Context { get; set; }
    public bool UseMultiplesFiles { get; set; }
    
    public SpeakMuchOptions(){}
    public SpeakMuchOptions(
        string resourcesPath,
        string resourceName,
        string languages,
        string language,
        string context = null,
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