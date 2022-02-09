using System.Numerics;
using System.IO;
using System.Globalization;
using System;

static public class Settings
{
    static public bool FirstRound { get; set; }

    static public bool Asteroids { get; set; }

    static public Vector2 Resolution { get; set; }

    static string[] WindowSize = File.ReadAllLines(@"resolution.txt");

    static public bool Lazer { get; set; }

    static Settings()
    {
        Resolution = new Vector2((int.Parse(WindowSize[0])), (int.Parse(WindowSize[1])));
        string[] windowSize = { $"{Resolution.X}", $"{Resolution.Y}" };
        File.WriteAllLines(@"resolution.txt", windowSize);
        FirstRound = false;
        Asteroids = true;
        Lazer = false;
    }
}