using System.Numerics;
using System;


public class Destroyed
{

    Vector2 position;

    float radius = 3;

    Vector2 speed = new Vector2(0, 0);

    Vector2 rotation = new Vector2((float)Math.Cos(0), (float)Math.Sin(0));

    Random generator = new Random();

    public Destroyed(Vector2 position, Vector2 speed, Vector2 rotation)
    {
        this.position = position;
        this.speed.X = speed.X + generator.Next(-2, 2);
        this.speed.Y = speed.Y + generator.Next(-2, 2);
        this.rotation.X = rotation.X + generator.Next(-1, 1);
        this.rotation.Y = rotation.Y + generator.Next(-1, 1);
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public float GetSize()
    {
        return radius;
    }

    public void DrawDestroyed(){
        
    }

}