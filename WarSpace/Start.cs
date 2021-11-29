using System;
using Raylib_cs;
using System.Numerics;
public class Start : Scene
{
    int selector = 0;

    Rocket mainRocket = new Rocket(new Vector2(250, 250), 250);
    public Start()
    {
        totalStars = generator.Next(100, 200);
        for (int i = 0; i <= totalStars; i++)
        {
            Star starcurrent = new Star();
            Starcollection.Add(starcurrent);
        }
    }

    public override void Draw()
    {
        base.Draw();
        foreach (Star c in Starcollection)
        {
            c.DrawStar();
        }
        mainRocket.DrawRocket();
    }

    public override void Update()
    {
        base.Update();
        mainRocket.Movement();
    }
}