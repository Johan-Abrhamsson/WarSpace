using System.Numerics;
using System;
using Raylib_cs;
using System.IO;

public class Star
{
    float starForm;

    Vector2 center;
    Random generator = new Random();
    int distanceColoration;

    Color distance;
    string[] WindowSize = File.ReadAllLines(@"resolution.txt");


    public Star()
    {
        starForm = generator.Next(5, 15);
        center.X = generator.Next(-int.Parse(WindowSize[0]), int.Parse(WindowSize[0]));
        center.Y = generator.Next(-int.Parse(WindowSize[1]), int.Parse(WindowSize[1]));
        distanceColoration = generator.Next(90, 255);
        distance = new Color(distanceColoration, distanceColoration, distanceColoration, distanceColoration);
    }

    public double GetSize()
    {
        return starForm;
    }

    public void DrawStar()
    {
        Raylib.DrawCircleV(center, starForm, distance);
        center += new Vector2(0.03f, 0.03f);
        if (center.X - (2 * starForm) >= int.Parse(WindowSize[0]) || center.Y - (2 * starForm) >= int.Parse(WindowSize[1]))
        {
            center.X = generator.Next(-int.Parse(WindowSize[0]), int.Parse(WindowSize[0]));
            center.Y = generator.Next(-int.Parse(WindowSize[1]), int.Parse(WindowSize[1]));
            while (center.X >= 0 && center.Y >= 0)
            {
                center.X = generator.Next(-int.Parse(WindowSize[0]), int.Parse(WindowSize[0]));
                center.Y = generator.Next(-int.Parse(WindowSize[1]), int.Parse(WindowSize[1]));
            }

        }
    }


}