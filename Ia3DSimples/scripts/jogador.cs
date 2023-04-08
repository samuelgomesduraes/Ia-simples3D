using Godot;
using System;

public partial class jogador : CharacterBody3D
{
	public const float Speed = 8.0f;
	public const float JumpVelocity = 8.0f;
	public float MouseSensibilidade=0.07f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = 14.0f;
	public Camera3D Cam;
	public override void _Ready(){
		Cam=GetNode<Camera3D>("Camera3D");
		Input.MouseMode=Input.MouseModeEnum.Captured;
	}
	public override void _Input(InputEvent @event){
	if (@event is InputEventMouseMotion motionEvent){
		Vector3 rotDeg = RotationDegrees;
		rotDeg.Y-=motionEvent.Relative.X * MouseSensibilidade;
		RotationDegrees=rotDeg;
		rotDeg=Cam.RotationDegrees;
		rotDeg.X-=motionEvent.Relative.Y * MouseSensibilidade;
		rotDeg.X=Mathf.Clamp(rotDeg.X,-90,40);
		Cam.RotationDegrees=rotDeg;
	}
	}
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_cancel")){
			Input.MouseMode=Input.MouseModeEnum.Visible;
		}
		else if(Input.IsActionJustPressed("ui_accept")){
			Input.MouseMode=Input.MouseModeEnum.Captured;
		}
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (Input.IsActionJustPressed("space") && IsOnFloor())
			velocity.Y = JumpVelocity;

		Vector2 inputDir = Input.GetVector("a", "d", "w", "s");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	} 
	public void takeDamage(){
		
	}  
}
