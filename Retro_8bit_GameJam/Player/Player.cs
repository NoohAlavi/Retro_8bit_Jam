using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public Vector2 velocity = Vector2.Zero;
    [Export]
    public float speed;
    [Export]
    public float gravityForce;
    [Export]
    public float jumpForce;
    [Export]
    public int maxJumps;
    [Export]
    public bool canReverseGravity = false;

    public string power;

    private Sprite sprite;
    private AnimationPlayer animation;
    private CPUParticles2D groundParticles;
    private AudioStreamPlayer2D jumpSound;
    private AudioStreamPlayer2D landSound;
    private AudioStreamPlayer dieSound;

    private int jumps;
    private bool wasGrounded = false;
    private bool isDead = false;

    public override void _Ready()
    {
        GD.Randomize();
        
        jumps = maxJumps;
        sprite = GetNode<Sprite>("Sprite");
        animation = GetNode<AnimationPlayer>("AnimationPlayer");
        groundParticles = GetNode<CPUParticles2D>("GroundParticles");
        jumpSound = GetNode<AudioStreamPlayer2D>("JumpSound");
        landSound = GetNode<AudioStreamPlayer2D>("LandSound");
        dieSound = GetNode<AudioStreamPlayer>("DieSound");

        dieSound.Connect("finished", this, nameof(RestartScene));

        float randomNumber = Mathf.Round(Convert.ToSingle(GD.RandRange(0, 2)));

        GD.Print(randomNumber);
        switch(randomNumber)
        {
            case 0:
                power = "Double Jump";
                maxJumps = 2;
                break;
            
            case 1:
                power = "Speed Boost";
                maxJumps = 1;
                speed *= 2f;
                break;

            case 2:
                power = "Reverse Gravity";
                canReverseGravity = true;
                break;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        velocity.y += gravityForce;
        velocity = MoveAndSlide(velocity, Vector2.Up);

        if (IsOnFloor())
        {
            if (!wasGrounded)
            {
                groundParticles.Restart();
                landSound.Play();
                // animation.Play("Land");
            }
            wasGrounded = true;
            jumps = maxJumps;
            animation.Play("Blink");
        }

        Vector2 screenBounds = GetViewport().Size;
        if (Position.y > screenBounds.y || Position.y < 0f)
        {
            Die();
        }

        if (velocity.x > 0)
        {
            sprite.FlipH = false;
        }
        else if (velocity.x < 0)
        {
            sprite.FlipH = true;
        }
    }

    public override void _Input(InputEvent @event)
    {
        velocity.x = (Input.GetActionStrength("MoveRight") - Input.GetActionStrength("MoveLeft")) * speed;

        if (Input.IsActionJustPressed("Jump") && jumps > 0)
        {
            jumpSound.PitchScale = Mathf.Clamp(GD.Randf(), 0.5f, 1f);
            jumpSound.Play();
            if (canReverseGravity)
            {
                gravityForce = -gravityForce;
                sprite.FlipV = !sprite.FlipV;
                return;
            }
            velocity.y = -jumpForce;
            jumps--;
            animation.Play("Jump");
            wasGrounded = false;
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            dieSound.Play(0f);
        }
        isDead = true;
        jumpForce = 0f;
        speed = 0f;
        canReverseGravity = false;
    }

    private void RestartScene()
    {
        GetTree().ReloadCurrentScene();
        //GetTree().ChangeScene("res://Game/Game.tscn");
    }
}
