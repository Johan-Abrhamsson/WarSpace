using System.ComponentModel;
using System;
using Raylib_cs;

SceneCollector begin = new SceneCollector();

const int windowWidth = 960;
const int windowHeight = 540;
const string windowTitle = "My cool window";

Raylib.InitWindow(windowWidth, windowHeight, windowTitle);
Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));

while (Raylib.WindowShouldClose() == false)
{
    Raylib.BeginDrawing();
    begin.PlayScene();
    Raylib.EndDrawing();
}

Raylib.CloseWindow();

