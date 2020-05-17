using Godot;
using System;
using System.Text.RegularExpressions;
using System.Linq;

public class Portal : Area2D
{
    private AnimationPlayer animation;
    [Export]
    private NodePath pathToAnimation;
    [Export]
    public int level;

    public override void _Ready()
    {
        animation = GetNode(pathToAnimation).GetNode<AnimationPlayer>("AnimationPlayer");
        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    private void OnBodyEntered(PhysicsBody2D body)
    {
        if (body is Player)
        {
            Player player = body as Player;
            player.gravityForce = 0f;
            player.speed = 0f;
            player.velocity = Vector2.Zero;
            animation.Play("Win");

            Save();
        }
    }

    private void Save()
    {
        var saveFile = new File();
        GD.Print(saveFile.Open("user://save_game.txt", File.ModeFlags.Write));
        if (!saveFile.FileExists("user://save_game.txt") || saveFile.GetLine() == "")
        {
            string str = saveFile.GetLine();
            str = str.Where(v => char.IsDigit(v)).ToString();
            GD.Print(str);
            if (str.ToInt() <= level)
            {
                saveFile.StoreLine((level + 1).ToString());
            }
        }
        else
        {
            saveFile.StoreLine((level + 1).ToString());
        }
        saveFile.Close();
    }
}