using System;
using System.Numerics;
using Raylib_cs;

public class Shot : Object
{

    string playerPolarity = "";

    public Shot(Vector2 position, Vector2 speed, Vector2 rotation, string playerPolarity)
    {
        radius = 5;
        this.position = position;
        this.speed = speed;
        this.rotation = rotation;
        this.playerPolarity = playerPolarity;
        if (playerPolarity == "player1")
        {
            color = Color.ORANGE;
        }
        else
        {
            color = Color.YELLOW;
        }
    }

    public void DrawShot()
    {
        Raylib.DrawCircleV(position, radius, color);
    }

    public void ShotRun()
    {
        position.X += speed.X;
        position.Y += speed.Y;
    }
    public string GetPolarity()
    {
        return playerPolarity;
    }
}
