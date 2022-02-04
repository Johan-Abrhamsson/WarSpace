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

    ClickBox colour = new ClickBox("Advanced", 0, 0, 0, 0, Color.WHITE, Color.BLUE);

    ClickBox multiplayer = new ClickBox("Mutiplayer", 0, 0, 0, 0, Color.WHITE, Color.GREEN);

    ClickBox botFight = new ClickBox("Bot Fight", 0, 0, 0, 0, Color.WHITE, Color.GREEN);

    ClickBox back = new ClickBox("Back", 0, 0, 0, 0, Color.RED, Color.GRAY);

    ClickBox resolution = new ClickBox("Resolution", 0, 0, 0, 0, Color.WHITE, Color.GREEN);

    ClickBox asteroids = new ClickBox("Asteroid", 0, 0, 0, 0, Color.WHITE, Color.GREEN);

    public Start()
    {
        //this.ChangeSceneCollector(begin);

        //Rocket placement
        mainRocket.ChangeRocketPos(new Vector2(-int.Parse(WindowSize[0]) / 2, int.Parse(WindowSize[1]) / 2));

        //Start game button
        startGame.ChangeClickBoxPos(new Vector2(int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        startGame.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Settings button
        settings.ChangeClickBoxPos(new Vector2(3.5f * int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        settings.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Colour button
        colour.ChangeClickBoxPos(new Vector2(6 * int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        colour.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Multiplayer button
        multiplayer.ChangeClickBoxPos(new Vector2(int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        multiplayer.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Bot fight button
        botFight.ChangeClickBoxPos(new Vector2(3.5f * int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        botFight.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //back button
        back.ChangeClickBoxPos(new Vector2(6 * int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        back.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Resolution button
        resolution.ChangeClickBoxPos(new Vector2(int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        resolution.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

        //Asteroids button
        asteroids.ChangeClickBoxPos(new Vector2(3.5f * int.Parse(WindowSize[0]) / 8, 3 * int.Parse(WindowSize[1]) / 4));
        asteroids.ChangeClickBoxSize(new Vector2(int.Parse(WindowSize[0]) / 9, int.Parse(WindowSize[1]) / 9));

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
            colour.DrawClickBox(true);
        }
    }

    public override void Update()
    {
        base.Update();
        mainRocket.Movement();
        if (mainRocket.GetRocketPos().X >= int.Parse(WindowSize[0]) + int.Parse(WindowSize[0]) / 2)
        {
            mainRocket.ChangeRocketPos(new Vector2(-int.Parse(WindowSize[0]) / 2, int.Parse(WindowSize[1]) / 2));
            if (keyhole == false)
            {
                buttons.Add(startGame);
                buttons.Add(settings);
                buttons.Add(colour);
                keyhole = true;
            }
        }
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
                        Program.startingGame.group.AddScene(new OtherScene(new List<ClickBox>() { multiplayer, botFight, back }));
                        break;
                    case "Settings":
                        Program.startingGame.group.AddScene(new OtherScene(new List<ClickBox>() { resolution, asteroids, back }));
                        break;
                    case "Advanced":
                        //Program.startingGame.group.AddScene("AdvancedMenu", new OtherScene(new List<ClickBox>() { multiplayer, botFight, back }));
                        //Program.startingGame.group.CurrentScene = "AdvancedMenu";
                        break;
                }
            }
        }
    }
}
