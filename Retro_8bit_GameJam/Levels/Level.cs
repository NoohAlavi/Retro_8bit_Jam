using Godot;
using System;

public class Level : Node2D
{
    [Export]
    public NodePath pathToContinueButton;
    
    private Player player;
    private Label showSkill;
    private Button continueButton;

    private Timer timer;
    private Label timeLabel;

    public override void _Ready()
    {
        player = GetNode<Player>("Player");
        
        continueButton = GetNode<Button>(pathToContinueButton);
        continueButton.Connect("pressed", this, nameof(LoadLevelSelect));
        
        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, nameof(OnTimerTimeout));
        timeLabel = GetNode<Label>("Sky/TimeLabel");

        GD.Print(player.power);
        showSkill = GetNode<Label>("ShowSkill/SkillLabel");
        showSkill.Text = "Your power: " + player.power;
    }

    public override void _Process(float delta)
    {
        timeLabel.Text = "Time Left: " + Mathf.Round(timer.TimeLeft);
    }

    void OnTimerTimeout()
    {
        player.Die();
        timer.Stop();
    }

    void LoadLevelSelect()
    {
        GetTree().ChangeScene("res://Levels/LevelSelect.tscn");
    }
}
