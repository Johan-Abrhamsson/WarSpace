using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;


public class Text
{
    string text;
    Vector2 postion;
    public Text(string text, Vector2 position)
    {
        this.text = text;
        this.postion = position;
    }

    public void DrawText()
    {
        Raylib.DrawText(text, (int)postion.X, (int)postion.Y, (int)Settings.Resolution.X / 40, Color.WHITE);
    }
}