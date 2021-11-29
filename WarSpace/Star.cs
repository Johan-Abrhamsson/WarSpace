using System.Numerics;
using System;
using Raylib_cs;

public class Star
{
    float starform;

    Vector2 center;
    Random generator = new Random();
    int distanceColoration;

    Color distance;

    public Star()
    {
        starform = generator.Next(5, 15);
        center.X = generator.Next(0, 960);
        center.Y = generator.Next(0, 540);
        distanceColoration = generator.Next(90, 255);
        distance = new Color(distanceColoration, distanceColoration, distanceColoration, distanceColoration);
    }

    public double GetSize()
    {
        return starform;
    }

    public void DrawStar()
    {
        Raylib.DrawCircleV(center, starform, distance);
    }


}