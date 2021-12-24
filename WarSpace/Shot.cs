using System;
using System.Numerics;
using Raylib_cs;

public class Shot
{
    Vector2 position;

    Vector2 speed = new Vector2(0, 0);

    Vector2 rotation = new Vector2((float)Math.Cos(0), (float)Math.Sin(0));

    float radius = 3;

    public Shot(Vector2 position, Vector2 speed, Vector2 rotation)
    {
        this.position = position;
        this.speed = speed;
        this.rotation = rotation;
    }

    public void DrawShot()
    {
        Raylib.DrawCircleV(position, radius, Color.YELLOW);
    }

    public void ShotRun()
    {
        position += speed * rotation;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public float GetSize()
    {
        return radius;
    }

    public Vector2 AppledForce(Force force)
    {
        speed += force.GetForce();
        return speed;
    }
}
