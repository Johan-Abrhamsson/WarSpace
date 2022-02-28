using System.Numerics;
using System;
using Raylib_cs;
using System.IO;

public class Star : Object
{

    int distanceColoration;

    Color distance;



    public Star()
    {
        radius = generator.Next(5, 15);
        position.X = generator.Next(-int.Parse(WindowSize[0]), int.Parse(WindowSize[0]));
        position.Y = generator.Next(-int.Parse(WindowSize[1]), int.Parse(WindowSize[1]));
        distanceColoration = generator.Next(90, 255);
        distance = new Color(distanceColoration, distanceColoration, distanceColoration, distanceColoration);
        this.color = distance;
        this.angle = (float)Math.PI * ((float)generator.Next(1, 628) / 100);
    }


    public void DrawStar()
    {
        //Raylib.DrawCircleV(center, starForm, distance);
        this.Drawing(3, 2);
        position += new Vector2(0.03f, 0.03f);
        if (position.X - (2 * radius) >= int.Parse(WindowSize[0]) || position.Y - (2 * radius) >= int.Parse(WindowSize[1]))
        {
            position.X = generator.Next(-int.Parse(WindowSize[0]), int.Parse(WindowSize[0]));
            position.Y = generator.Next(-int.Parse(WindowSize[1]), int.Parse(WindowSize[1]));
            while (position.X >= 0 && position.Y >= 0)
            {
                position.X = generator.Next(-int.Parse(WindowSize[0]), int.Parse(WindowSize[0]));
                position.Y = generator.Next(-int.Parse(WindowSize[1]), int.Parse(WindowSize[1]));
            }

        }
    }


}