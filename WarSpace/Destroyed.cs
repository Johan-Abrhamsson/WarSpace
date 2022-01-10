using System.Numerics;
using System;
using Raylib_cs;


public class Destroyed : Object
{
    public Destroyed(Vector2 position, Vector2 speed, Vector2 rotation)
    {
        radius = generator.Next(1, 6);
        this.position = position;
        this.speed.X = speed.X + generator.Next(-2, 5);
        this.speed.Y = speed.Y + generator.Next(-2, 5);
        this.rotation.X = rotation.X + generator.Next((int)-(Math.PI), (int)(Math.PI));
        this.rotation.Y = rotation.Y + generator.Next((int)-(Math.PI), (int)(Math.PI));
        this.color = new Color(generator.Next(130, 255), generator.Next(0, 80), generator.Next(0, 40), 255);
    }


    public void DrawDestroyed()
    {
        Raylib.DrawCircleV(position, radius, color);
        position += speed * rotation;
    }

}