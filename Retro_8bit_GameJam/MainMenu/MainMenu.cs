using Godot;
using System;

public class MainMenu : Control
{
    public override void _Ready()
    {
        GetNode<Button>("CanvasLayer/PlayButton").Connect("pressed", this, nameof(StartGame));
    }

    void StartGame()
    {
        GetTree().ChangeScene("res://Levels/LevelSelect.tscn");
    }
}
