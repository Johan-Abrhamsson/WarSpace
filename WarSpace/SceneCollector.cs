using System.Reflection.PortableExecutable;
using System.Collections.Generic;
using System;

public static class SceneCollector
{
    Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

    Start start = new Start();

    public string CurrentScene { get; set; }
    
    public SceneCollector()
    {
        CurrentScene = "Start";
        scenes.Add("Start", start);
    }

    public void PlayScene()
    {
        scenes[CurrentScene].Draw();
        scenes[CurrentScene].Update();
    }

    public void AddScene(string name, Scene scene)
    {
        scenes.Add(name, scene);
    }


}