using Godot;
using System;

public partial class MainMenu : Node2D
{
	[Export] private Label _playButton;
    [Export] private Label _exitButton;
	[Export] private Label _optionsButton;
	
    public override void _Ready()
	{
		LocalizationManager.OnLanguageChanged += UpdateLocalization;
        UpdateLocalization();
	}

	public void OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://src/Levels/node_2d.tscn");
	}

	public void OnOptionsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://src/Interface/OptionsMenu/options_menu.tscn");
	}

	public void OnExitButtonPressed()
	{
		GetTree().Quit();
	}

	public void UpdateLocalization()
	{
		_playButton.Text = LocalizationManager.GetTranslatedString("menu_play");
		_optionsButton.Text = LocalizationManager.GetTranslatedString("menu_options");
		_exitButton.Text = LocalizationManager.GetTranslatedString("menu_exit");
	}
	
    protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			LocalizationManager.OnLanguageChanged -= UpdateLocalization;
		}
		base.Dispose(disposing);
	}
}
