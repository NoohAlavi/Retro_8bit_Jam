using Godot;
using System;

public class Intro : Control
{
    private Button nextButton;

    public override void _Ready()
    {
        nextButton = GetNode<Button>("CanvasLayer/NextButton");
        nextButton.Connect("pressed", this, nameof(Next));

        GetNode<AnimationPlayer>("AnimationPlayer").Connect("animation_finished", this, nameof(ChangeText));
    }

    void Next()
    {
        GetTree().ChangeScene("res://MainMenu/MainMenu.tscn");
    }

    void ChangeText(String anim)
    {
        nextButton.Text = "Continue";
    }
}
