using Godot;
using System;

public partial class enemy : CharacterBody3D
{
    public CharacterBody3D Jogador;
    public Timer Tempo;
    public float AttackDist=1.5f;
    public float AttackRate=1.0f;
    public float Movespeed=3.5f;

    public override void _Ready(){
        Jogador=GetParent().GetNode<CharacterBody3D>("jogador");
        Tempo=GetNode<Timer>("Timer");
        Tempo.WaitTime=AttackRate;
        Tempo.Start();
    }
    public override void _Process(double delta){
       Vector3 Vel=Velocity;
        var dist=GlobalPosition.DistanceTo(Jogador.GlobalPosition);
        if(dist > AttackDist){
            var dir=(Jogador.GlobalPosition-GlobalPosition).Normalized();
            Vel.X=dir.X*Movespeed;
            Vel.Y=0;
            Vel.Z=dir.Z*Movespeed;
        } Velocity=Vel;
        MoveAndSlide();
    }
    private void _on_timer_timeout(){
        if(Position.DistanceTo(Jogador.GlobalPosition) <= AttackDist){
            //Jogador.takeDamage(damage);
        }
    }
}
