using System;
using Raylib_cs;
using System.IO;


public class Screen
{
    public int WindowWidth { get; set; }
    public int WindowHeight { get; set; }
    const string windowTitle = "War Space";



    public Screen(int width, int heigt)
    {
        this.WindowWidth = width;
        this.WindowHeight = heigt;
        string[] windowSize = { $"{this.WindowWidth}", $"{this.WindowHeight}" };
        File.WriteAllLines(@"resolution.txt", windowSize);
        Raylib.InitWindow(WindowWidth, WindowHeight, windowTitle);
        Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));
        Raylib.SetMasterVolume(1f);
        Raylib.InitAudioDevice();
    }

    //What should be rendered
    public void Run(SceneCollector playersScene, string name)
    {

        while (Raylib.WindowShouldClose() == false)
        {
            playersScene.PlayMusic();
            Raylib.BeginDrawing();
            playersScene.PlayScene();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();


    }
}