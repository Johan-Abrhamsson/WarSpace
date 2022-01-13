using System.Globalization;
using System;

static public class Settings
{
    static public bool FirstRound { get; set; }

    static public bool Asteroids { get; set; }

    static Settings()
    {
        FirstRound = false;
        Asteroids = true;
    }
}