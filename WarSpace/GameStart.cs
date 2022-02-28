using System;
using Raylib_cs;

public class GameStart
{
    Screen mainWindow = new Screen((int)Settings.Resolution.X, (int)Settings.Resolution.Y);

    public SceneCollector group = new SceneCollector();

    public void Start()
    {
        mainWindow.Run(group, "");

    }


}