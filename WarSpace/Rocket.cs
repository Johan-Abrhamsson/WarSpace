using System.Numerics;
using System;
using Raylib_cs;
public class Rocket
{
    Vector2 position;

    Vector2 speed = new Vector2(0, 0);

    Vector2 accileration = new Vector2(0.03f, 0.03f);

    double angle;

    Vector2 rotation = new Vector2((float)Math.Acos(0), (float)Math.Asin(0));


    bool validMovement = true;

    public Rocket(Vector2 positionNew, double angleNew, bool validMovementNew)
    {
        this.position = positionNew;
        this.angle = angleNew;
        this.rotation.X = (float)Math.Cos(angle);
        this.rotation.Y = (float)Math.Sin(angle);
        this.validMovement = validMovementNew;
    }

    public void Movement()
    {
        if (validMovement == true)
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                speed += accileration;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                speed -= accileration;
                if (speed.X <= 0 && speed.Y <= 0)
                {
                    speed = new Vector2(0, 0);
                }
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                angle += (double)accileration.X;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                angle -= (double)accileration.X;
            }
        }
        rotation.X = (float)Math.Cos(angle);
        rotation.Y = (float)Math.Sin(angle);
        position.X += speed.X * rotation.X;
        position.Y += speed.Y * rotation.Y;
    }

    public bool Control(bool value)
    {
        validMovement = value;
        return validMovement;
    }

    public void DrawRocket()
    {
        Raylib.DrawLine((int)position.X, (int)position.Y, (int)(position.X + rotation.X * 20), (int)(position.Y + rotation.Y * 20), Color.RED);
        Raylib.DrawLine((int)position.X, (int)position.Y - 5, (int)(position.X + speed.X * 5), (int)position.Y - 5, Color.GREEN);
    }


    public Vector2 GetRocketPos()
    {
        return position;
    }
    public Vector2 ChangeRocketPos(Vector2 Change)
    {
        this.position = Change;
        return position;
    }

    public Vector2 AppledForce(Force force)
    {
        speed += force.GetForce();
        return speed;
    }
}