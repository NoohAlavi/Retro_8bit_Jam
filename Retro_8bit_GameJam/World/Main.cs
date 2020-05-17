using Godot;
using System;

public class Main : Node2D
{
    [Export]
    public PackedScene gameScene;
    [Export]
    public bool gameOn = false;
    [Export]
    public NodePath powerButton;

    public override void _Ready()
    {
        GetNode<Button>(powerButton).Connect("pressed", this, nameof(PowerTV));
    }

    private void PowerTV()
    {
        gameOn = !gameOn;
        if (gameOn)
        {
            AddChild(gameScene.Instance());
        }
        else
        {
            GetNode("Game").QueueFree();
        }
    }
}
