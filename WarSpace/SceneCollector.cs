using System.Reflection.PortableExecutable;
using System.Collections.Generic;
using System;

public class SceneCollector
{
    Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

    Start start = new Start();

    public string currentScene {get; set;}

    public SceneCollector()
    {
        currentScene = "Start";
        scenes.Add("Start", start);
    }

    public void PlayScene()
    {
        scenes[currentScene].Draw();
        scenes[currentScene].Update();
    }

    public void AddScene()
    {

    }


}