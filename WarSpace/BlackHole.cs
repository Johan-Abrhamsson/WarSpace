using Raylib_cs;
using System.Security.AccessControl;
using System;
using System.Numerics;

public class BlackHole : Object
{
    Vector2 pullVector;
    public BlackHole(Vector2 position, float mass)
    {
        this.radius = mass;
        this.position = position;
        this.color = Color.PURPLE;
    }

    public Vector2 Pull(Object value)
    {
        pullVector = this.position - value.GetPosition();
        return value.AppledForce(new Force((radius / 1000000), pullVector));
    }

    public void DrawBlackHole()
    {
        //Raylib.DrawCircleV(position, radius, color);
        this.Drawing(2, 2);
        this.angle += 0.01f;
    }
}
