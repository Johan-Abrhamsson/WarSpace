using System;

public class GameStart
{
    Screen mainWindow = new Screen(500, 900);

    public SceneCollector group = new SceneCollector();

    public void Start()
    {
        mainWindow.Run(group);

    }


}