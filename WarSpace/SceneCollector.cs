using System.Reflection.PortableExecutable;
using System.Collections.Generic;
using System;
using Raylib_cs;

public class SceneCollector
{
    List<Scene> scenes = new List<Scene>();

    Start start = new Start();

    int currentScene;

    public Music currentMusic = Raylib.LoadMusicStream("Rockiter.ogg");


    //the -1 is due to it otherwice would start at 0 and try to run the scene of index 1, which is non exsistent
    public SceneCollector()
    {
        currentScene = -1;
        this.AddScene(start);
    }

    //What scene that should be running
    public void PlayScene()
    {
        scenes[currentScene].Draw();
        scenes[currentScene].Update();
    }

    //if a scene needs to be added
    public void AddScene(Scene scene)
    {
        scenes.Add(scene);
        currentScene++;
    }

    public void PlayMusic()
    {
        Raylib.PlayMusicStream(currentMusic);
        Raylib.UpdateMusicStream(currentMusic);
    }
}