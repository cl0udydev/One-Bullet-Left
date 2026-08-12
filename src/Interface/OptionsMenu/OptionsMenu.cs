using Godot;
using System;

public partial class OptionsMenu : Node2D
{
	[Export] private HSlider _masterVolumeSlider;
    [Export] private Label _soundLabel;
    [Export] private Label _langLabel;
    [Export] private Sprite2D _langButtonSprite;
    private const string BusName = "Master";
    private int _busIndex;

    public override void _Ready()
    {
        LocalizationManager.OnLanguageChanged += UpdateLocalization;
        UpdateLocalization();

        _busIndex = AudioServer.GetBusIndex(BusName);

        if (_busIndex == -1)
        {
            return;
        }

        float currentDb = AudioServer.GetBusVolumeDb(_busIndex);
        _masterVolumeSlider.Value = currentDb;

        _masterVolumeSlider.ValueChanged += OnVolumeSliderChanged;
    }

    private void OnVolumeSliderChanged(double value)
    {
        if (value <= _masterVolumeSlider.MinValue)
        {
            AudioServer.SetBusMute(_busIndex, true); 
        }
        else
        {
            AudioServer.SetBusMute(_busIndex, false);
            
            AudioServer.SetBusVolumeDb(_busIndex, (float)value);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && IsInstanceValid(_masterVolumeSlider))
        {
            _masterVolumeSlider.ValueChanged -= OnVolumeSliderChanged;
        }
        base.Dispose(disposing);
    }


	public void OnBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://src/Interface/MainMenu/main_menu.tscn");
	}

    public void OnLangButtonPressed()
    {
        if (LocalizationManager.CurrentLanguage == "RU")
        {
            LocalizationManager.LoadLanguage("EN");
            UpdateLocalization();
            _langButtonSprite.Texture =  GD.Load<Texture2D>("res://assets/Interface/ui_eng.png");
        }
        else
        {
            LocalizationManager.LoadLanguage("RU");
            UpdateLocalization();
            _langButtonSprite.Texture =  GD.Load<Texture2D>("res://assets/Interface/ui_rus.png");
        }
    }
    public void UpdateLocalization()
	{
		_soundLabel.Text = LocalizationManager.GetTranslatedString("options_sound");
        _langLabel.Text = LocalizationManager.GetTranslatedString("options_lang");
	}
}
