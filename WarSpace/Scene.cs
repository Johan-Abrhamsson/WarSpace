using System;
using Raylib_cs;
using System.Collections.Generic;

public class Scene
{
    protected int totalStars;
    protected List<Star> Starcollection = new List<Star>();
    protected Random generator = new Random();
    protected SceneCollector collection;

    public Scene()
    {

    }

    virtual public void Update()
    {

    }
    virtual public void Draw()
    {
        Raylib.ClearBackground(Color.BLACK);
    }
}
