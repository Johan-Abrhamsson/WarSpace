using System;

public static class GameStart
{
    Screen mainWindow = new Screen(1280, 900);

    static SceneCollector group = new SceneCollector();

    public GameStart()
    {
        mainWindow.Run(group);

    }


}