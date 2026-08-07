using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
public partial class LocalizationManager : Node
{
    private static Dictionary<string, string> _currentTranslations = new();
	public static event Action OnLanguageChanged;
	public static string CurrentLanguage;

    public override void _Ready()
    {
        LoadLanguage("EN");
		CurrentLanguage = "EN";
    }

    public static void LoadLanguage(string langCode)
    {
        string path = $"res://src/Core/Localization/{langCode}.json";

        if (!FileAccess.FileExists(path))
        {
            return;
        }

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        string jsonText = file.GetAsText();

        try
        {
            _currentTranslations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);
			CurrentLanguage = langCode;
			OnLanguageChanged?.Invoke();
        }
        catch (Exception ex)
        {
            GD.PrintErr($"{ex.Message}");
        }
    }

    public static string GetTranslatedString(string key)
    {
        if (_currentTranslations.TryGetValue(key, out string translatedValue))
        {
            return translatedValue;
        }
        
        return $"[{key}]";
    }
}
