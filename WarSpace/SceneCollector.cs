using System.Reflection.PortableExecutable;
using System.Collections.Generic;
using System;

public class SceneCollector
{
    Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

    List<string> nameList = new List<string>();

    Start start = new Start();

    public string CurrentScene { get; set; }

    int checkValue = 0;

    public SceneCollector()
    {
        CurrentScene = "Start";
        this.AddScene("Start", start);
    }

    public void PlayScene()
    {
        scenes[CurrentScene].Draw();
        scenes[CurrentScene].Update();
    }

    public void AddScene(string name, Scene scene)
    {
        for (int i = 0; i < nameList.Count; i++)
        {
            if (name != nameList[i])
            {
                checkValue++;
            }
        }
        if (checkValue == nameList.Count)
        {
            scenes.Add(name, scene);
            nameList.Add(name);
        }
        checkValue = 0;
    }
}