using System.Diagnostics;
using System;
using System.Numerics;
using Raylib_cs;

public class Shot : Object
{

    string playerPolarity = "";
    float newSpeed;

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
        position = position * rotation;

        this.newSpeed = (speed.X + speed.Y / 2);
        this.newSpeed = CalcSpeed();
        this.angle = (float)Math.Asin((double)rotation.Y);
        //Could fix the small issue here but it is fine
    }

    public void DrawShot()
    {
        //Raylib.DrawCircleV(position, radius, color);
        this.Drawing(1, 3);
    }

    public void ShotRun()
    {
        position += newSpeed * rotation;
    }
    public string GetPolarity()
    {
        return playerPolarity;
    }

    private float CalcSpeed()
    {
        // See if the speed from the player is negative
        // If it is, make it positibe with -1
        float tempSpeed = newSpeed > 0 ? newSpeed : newSpeed * -1;

        // Add the constant speed of 5 to the variable
        tempSpeed += 5;

        //if lazer is on
        if (Settings.Lazer)
            tempSpeed += 5;


        // Return the speed
        return tempSpeed;
    }
}
