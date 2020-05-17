using Godot;
using System;

public class LevelSelect : Control
{

    private int savedLevel;

    public override void _Ready()
    {
        foreach (Button button in GetNode("Buttons").GetChildren())
        {
            button.Connect("pressed", this, nameof(LoadLevel), new Godot.Collections.Array(new[] { button }));
        }

        LoadSave();
    }

    void LoadLevel(Button btn)
    {
        GetTree().ChangeScene("res://Levels/" + btn.Name + ".tscn");
    }

    void LoadSave()
    {
        File saveFile = new File();
        GD.Print(saveFile.Open("user://save_game.txt", File.ModeFlags.Read));
        if (!saveFile.FileExists("user://save_game.txt") || saveFile.GetLine() == "")
        {
            savedLevel = 1;
            saveFile.Open("user://save_game.txt", File.ModeFlags.Write);
            saveFile.StoreLine("1");
            saveFile.Close();
            return;
        }
        savedLevel = saveFile.GetLine().ToInt();
        GD.Print("Level: " + savedLevel);
        saveFile.Close();
    }
}
