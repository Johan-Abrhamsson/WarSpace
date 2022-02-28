using System.Numerics;
using System;
using Raylib_cs;


public class Destroyed : Object
{
    int distance;
    public Destroyed(Vector2 position, Vector2 speed, Vector2 rotation)
    {
        radius = generator.Next(5, 10);
        this.position = position;
        this.speed.X = speed.X + generator.Next(-2, 3);
        this.speed.Y = speed.Y + generator.Next(-2, 3);
        this.distance = 255;
        this.rotation.X = rotation.X + (float)Math.PI * ((float)generator.Next(1, 628) / 200);
        this.rotation.Y = rotation.Y + (float)Math.PI * ((float)generator.Next(1, 628) / 200);
        this.angle = (float)Math.Asin((double)(rotation.Y));
        this.color = new Color(generator.Next(130, 255), generator.Next(0, 80), generator.Next(0, 40), distance);
    }


    public void DrawDestroyed()
    {
        //Raylib.DrawCircleV(position, radius, color);
        this.Drawing(3, 1);
        position += speed * rotation;
        distance -= generator.Next(100, 100);
        if (distance < 0)
        {
            distance = 0;
        }

    }

}