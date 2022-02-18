using System;
using Raylib_cs;
using System.Numerics;
using System.IO;
using System.Collections.Generic;
public class Start : Scene
{

    Rocket mainRocket = new Rocket(new Vector2(0, 0), 0, false, "-");

    bool keyhole = false;

    List<ClickBox> buttons = new List<ClickBox>();

    ClickBox startGame = new ClickBox("Start Game", 0, 0, 0, 0, Color.WHITE, Color.GREEN);

    ClickBox settings = new ClickBox("Settings", 0, 0, 0, 0, Color.WHITE, Color.YELLOW);

    ClickBox controls = new ClickBox("Controls", 0, 0, 0, 0, Color.WHITE, Color.BLUE);

    public Start()
    {

        //Rocket placement
        mainRocket.ChangeRocketPos(new Vector2(-int.Parse(WindowSize[0]) / 2, int.Parse(WindowSize[1]) / 2));

        //Start game button
        startGame.ChangeClickBoxPos(new Vector2(int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        startGame.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Settings button
        settings.ChangeClickBoxPos(new Vector2(3.5f * int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        settings.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Controls button
        controls.ChangeClickBoxPos(new Vector2(6 * int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        controls.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Force on Rocket
        mainRocket.AppledForce(new Force(int.Parse(WindowSize[0]) / 100, new Vector2(1, 0)));

    }

    public override void Draw()
    {
        base.Draw();

        mainRocket.DrawRocket();
        if (keyhole == true)
        {
            Raylib.DrawText("War", int.Parse(WindowSize[0]) / 6, int.Parse(WindowSize[1]) / 4, int.Parse(WindowSize[0]) / 8, Color.WHITE);
            Raylib.DrawText("Space", 3 * int.Parse(WindowSize[0]) / 6, int.Parse(WindowSize[1]) / 4, int.Parse(WindowSize[0]) / 8, Color.WHITE);

            startGame.DrawClickBox(true);
            settings.DrawClickBox(true);
            controls.DrawClickBox(true);
        }
    }

    public override void Update()
    {
        base.Update();
        mainRocket.Movement();
        //Fixes the buttons
        if (mainRocket.GetRocketPos().X >= int.Parse(WindowSize[0]) + int.Parse(WindowSize[0]) / 2)
        {
            mainRocket.ChangeRocketPos(new Vector2(-int.Parse(WindowSize[0]) / 2, int.Parse(WindowSize[1]) / 2));
            if (keyhole == false)
            {
                buttons.Add(startGame);
                buttons.Add(settings);
                buttons.Add(controls);
                keyhole = true;
            }
        }
        //What button is hoverd/ clicked
        Buttoncheck();
    }

    void Buttoncheck()
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].ClickBoxColorHover(mousePos.X, mousePos.Y);
            if (buttons[i].Check(mousePos.X, mousePos.Y) == true)
            {
                switch (buttons[i].GetText())
                {
                    case "Start Game":
                        Program.startingGame.group.AddScene(new OtherScene(new List<ClickBox>() { new ClickBox("Multiplayer", 0, 0, 0, 0, Color.RAYWHITE, Color.GREEN), new ClickBox("Bot Fight", 0, 0, 0, 0, Color.RAYWHITE, Color.GREEN), new ClickBox("Back", 0, 0, 0, 0, Color.RED, Color.GRAY) }, "Only multiplayer works right now, sorry"));
                        break;
                    case "Settings":
                        Program.startingGame.group.AddScene(new OtherScene(new List<ClickBox>() { new ClickBox("Resolution", 0, 0, 0, 0, Color.RAYWHITE, Color.YELLOW), new ClickBox("Asteroid", 0, 0, 0, 0, Color.RAYWHITE, Color.YELLOW), new ClickBox("Lazer", 0, 0, 0, 0, Color.RAYWHITE, Color.YELLOW), new ClickBox("Back", 0, 0, 0, 0, Color.RED, Color.GRAY) }, $"Settings, toggle asteroids: [{Settings.Asteroids}] | toggle resolution: [{Settings.Resolution.X}] / [{Settings.Resolution.Y}] | toggle lazer: [{Settings.Lazer}] | Go back to start to see updates"));
                        break;
                    case "Controls":
                        Program.startingGame.group.AddScene(new OtherScene(new List<ClickBox>() { new ClickBox("Back", 0, 0, 0, 0, Color.RED, Color.GRAY) }, "Player1 Movement: Arrows, Shoot: '.' | Player2 Movement: 'WASD', Shoot: 'G'"));
                        break;
                }
            }
        }
    }
}
