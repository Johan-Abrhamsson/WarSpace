using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;


public class Text
{
    string text;
    Vector2 postion;
    float size;
    public Text(string text, Vector2 position, float size)
    {
        this.text = text;
        this.postion = position;
        this.size = size;
    }

    public void DrawText()
    {
        Raylib.DrawText(text, (int)postion.X, (int)postion.Y, (int)(Settings.Resolution.X / 40 * size), Color.WHITE);
    }
}