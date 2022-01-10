using System.Reflection.PortableExecutable;
using System.Collections.Generic;
using System;

public class SceneCollector
{
    List<Scene> scenes = new List<Scene>();

    Start start = new Start();

    int currentScene;

    public SceneCollector()
    {
        currentScene = -1;
        this.AddScene(start);
    }

    public void PlayScene()
    {
        scenes[currentScene].Draw();
        scenes[currentScene].Update();
    }

    public void AddScene(Scene scene)
    {
        scenes.Add(scene);
        currentScene++;
    }
}