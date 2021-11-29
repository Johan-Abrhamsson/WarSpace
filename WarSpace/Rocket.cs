using System.Numerics;
using System;
using Raylib_cs;

public class Rocket
{
    Vector2 position;

    Vector2 speed = new Vector2(0, 0);

    Vector2 accileration = new Vector2(0.05f, 0.05f);

    float angle;

    bool validMovement = true;

    public Rocket(Vector2 positionNew, float angleNew)
    {
        this.position = positionNew;
        this.angle = angleNew;
    }

    public void Movement()
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            speed += accileration;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            speed -= accileration;
        }
        position += speed;
    }

    public bool Control(bool value)
    {
        validMovement = value;
        return validMovement;
    }

    public void DrawRocket()
    {
        Raylib.DrawCircleV(position, 20, Color.RED);
        Raylib.DrawLine((int)position.X, (int)position.Y, (int)(position.X + speed.X), (int)(position.Y + speed.Y), Color.GREEN);
    }
}