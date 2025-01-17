using SpeakMuch.Implementations;

namespace SpeakMuch.Interfaces;

public interface ISpeakMuchService
{
    string this[string name] { get; }
    string this[string name, params object[] arguments] { get; }    
    SpeakMuchService WithPath(string path);
    SpeakMuchService WithFile(string file);
    SpeakMuchService WithContext(string context);
    SpeakMuchService WithCulture(string culture);
    SpeakMuchService WithMultipleFiles(bool multipleFiles);
    SpeakMuchService Load();
    SpeakMuchService WithFileResource(string path, string name, string context = null, string culture = null);
}