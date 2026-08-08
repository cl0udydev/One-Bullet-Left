using Godot;
using System;

public partial class OptionsMenu : Node2D
{
	public void OnBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://src/Interface/MainMenu/main_menu.tscn");
	}

}
