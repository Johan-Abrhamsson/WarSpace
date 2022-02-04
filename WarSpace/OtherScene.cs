using System.Globalization;
using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

public class OtherScene : Scene
{
    List<ClickBox> buttons;

    float distance;
    public OtherScene(List<ClickBox> newButtons)
    {
        this.buttons = newButtons;
        distance = (int.Parse(WindowSize[1]) / 2) / buttons.Count;
        for (int i = 0; i <= buttons.Count - 1; i++)
        {
            buttons[i].ChangeClickBoxPos(new Vector2((int.Parse(WindowSize[0])) / 7, ((int.Parse(WindowSize[0])) / 5) + (i * distance)));
        }
    }

    public override void Draw()
    {
        base.Draw();
        foreach (ClickBox C in buttons)
        {
            C.DrawClickBox(true);
        }
    }
    public override void Update()
    {
        base.Update();
        Buttoncheck();
    }

    void Buttoncheck()
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].ClickBoxColorHover(mousePos.X, mousePos.Y);
            if (buttons[i].Check(mousePos.X, mousePos.Y))
            {
                switch (buttons[i].GetText())
                {
                    case "Mutiplayer":
                        Program.startingGame.group.AddScene(new Battle("player1", "player2"));
                        break;
                    case "Asteroid":
                        Settings.Asteroids = !Settings.Asteroids;
                        Console.WriteLine(Settings.Asteroids);
                        break;
                    case "Back":
                        Program.startingGame.group.AddScene(new Start());
                        break;
                }
            }
        }
    }
}