using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

public class OtherScene : Scene
{
    List<ClickBox> buttons;

    float distance;

    string text;

    string[] textComp;

    string command;

    int row;

    int collum;

    List<Text> textList = new List<Text>();
    public OtherScene(List<ClickBox> buttons, string text)
    {
        this.buttons = buttons;
        this.text = text;
        this.textComp = text.Split(' ');
        distance = (int.Parse(WindowSize[1]) / 2) / buttons.Count;
        for (int i = 0; i <= buttons.Count - 1; i++)
        {
            buttons[i].ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));
            buttons[i].ChangeClickBoxPos(new Vector2((int.Parse(WindowSize[0])) / 7, ((int.Parse(WindowSize[0])) / 5) + (i * distance)));
        }
        row = (int.Parse(WindowSize[1])) / 300;
        collum = 0;
        foreach (string C in textComp)
        {
            if ((int)distance / 8 * collum > (int.Parse(WindowSize[0])) / 2.2)
            {
                row += (int.Parse(WindowSize[1])) / 300;
                collum = 0;
            }
            //Command upcoming
            if (C.Contains('['))
            {
                //How long command
                for (int i = C.IndexOf('['); i < C.Length; i++)
                {
                    if (C[i] == ']')
                    {
                        command = "";
                        for (int k = C.IndexOf('[') + 1; k <= C.IndexOf(']') - 1; k++)
                        {
                            command += C[k];
                        }
                        break;
                    }
                }
                textList.Add(new Text(command, new Vector2(((int.Parse(WindowSize[0])) / 3f) + 15 * collum, 20 * row), 1));
            }
            else
            {
                textList.Add(new Text(C, new Vector2(((int.Parse(WindowSize[0])) / 3f) + 15 * collum, 20 * row), 1));
            }
            collum += (int.Parse(WindowSize[0])) / 100;
        }
    }

    public override void Draw()
    {
        base.Draw();
        foreach (ClickBox C in buttons)
        {
            C.DrawClickBox(true);
        }
        foreach (Text C in textList)
        {
            C.DrawText();
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
                    case "Multiplayer":
                        Program.startingGame.group.AddScene(new Battle("player1", "player2"));
                        break;
                    case "Asteroid":
                        Settings.Asteroids = !Settings.Asteroids;
                        break;
                    case "Resolution":
                        if (Settings.Resolution != new Vector2(640, 480))
                            Settings.Resolution = new Vector2(640, 480);

                        else
                            Settings.Resolution = new Vector2(1200, 900);

                        break;
                    case "Back":
                        Program.startingGame.group.AddScene(new Start());
                        break;
                    case "Lazer":
                        Settings.Lazer = !Settings.Lazer;
                        break;
                }
            }
        }
    }
}