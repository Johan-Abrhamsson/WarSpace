using System;
using Raylib_cs;
using System.Collections.Generic;
using System.IO;

public class Scene
{
    protected int totalStars;
    protected List<Star> Starcollection = new List<Star>();
    protected Random generator = new Random();
    protected string[] WindowSize = File.ReadAllLines(@"resolution.txt");
    protected int ticker = 0;
    // public Music currentMusic = Raylib.LoadMusicStream("Rockiter.ogg");
    public Scene()
    {
        totalStars = generator.Next((int.Parse(WindowSize[0]) * int.Parse(WindowSize[1])) / (3 * int.Parse(WindowSize[0]) + 3 * int.Parse(WindowSize[1])), int.Parse(WindowSize[0]) * int.Parse(WindowSize[1]) / (2 * int.Parse(WindowSize[0]) + 2 * int.Parse(WindowSize[1])));
        for (int i = 0; i <= totalStars; i++)
        {
            Star starcurrent = new Star();
            Starcollection.Add(starcurrent);
        }

    }

    //For all scenes for what should be exicuted.
    virtual public void Update()
    {
        ticker++;

        // Raylib.PlayMusicStream(currentMusic);
        // Raylib.UpdateMusicStream(currentMusic);
    }
    virtual public void Draw()
    {
        Raylib.ClearBackground(Color.BLACK);
        foreach (Star c in Starcollection)
        {
            c.DrawStar();
        }
    }
}
