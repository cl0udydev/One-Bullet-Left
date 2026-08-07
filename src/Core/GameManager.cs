using Godot;
using System;

public partial class GameManager : Node
{
	public override void _Ready()
	{
		Resource cursorTexture = GD.Load("res://assets/Hud/cursor.png");
		Vector2 hotspot = new Vector2(16, 16);

        Input.SetCustomMouseCursor(cursorTexture, Input.CursorShape.Arrow, hotspot);
	}
}
