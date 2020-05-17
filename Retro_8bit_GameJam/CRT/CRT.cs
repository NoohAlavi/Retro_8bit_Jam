using Godot;
using System;

public class CRT : CanvasLayer
{
    [Export]
    public bool isActive = true;

    public override void _Process(float delta)
    {
        // foreach (Node child in GetChildren())
        // {
        //     if (isActive)
        //     {
        //         child.Hide();
        //     }
        //     else
        //     {
        //         child.Show();
        //     }
        // }
    }
}