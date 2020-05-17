using Godot;
using System;

public class Game : Node2D
{
    [Export]
    public int level;


    private AcceptDialog showPower;
    private Player player;

    public override void _Ready()
    {
        player = GetNode<Player>("Player");

        GetNode<Button>("WinPopup/WinScreen/ContinueButton").Connect("pressed", this, nameof(OnButtonPressed));

        showPower = GetNode<AcceptDialog>("Popup/ShowPower");
        showPower.DialogText = "Your power:\n\n" + player.power;

        showPower.Popup_();
    }

    private void OnButtonPressed()
    {
        GetTree().ChangeScene("Game/Game.tscn");
        // LoadLevel(level + 1);
    }
}
