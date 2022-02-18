using System.Threading;
using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
public class Battle : Scene
{
    List<Shot> totalShot = new List<Shot>();

    List<Asteroid> totalAsteroid = new List<Asteroid>();

    List<BlackHole> totalBlackHole = new List<BlackHole>();

    Rocket rocket1 = new Rocket(new Vector2(0, 0), (float)((11 * Math.PI) / 6), true, "-");

    Rocket rocket2 = new Rocket(new Vector2(0, 0), (float)((5 * Math.PI) / 6), true, "-");

    BlackHole blackHole = new BlackHole(new Vector2(600, 600), 20);

    string player1;

    string player2;

    string winner = "not";

    public Battle(string player1Value, string player2Value)
    {
        //Gör så att spelet vet om spelaren är en bot eller inte
        this.player1 = player1Value;
        this.player2 = player2Value;
        //Fixar alla postioner
        rocket1.ChangePlayer(player1);
        rocket1.ChangeRocketPos(new Vector2((int.Parse(WindowSize[0])) * 0.1f, (int.Parse(WindowSize[1]) * 0.9f)));
        rocket2.ChangePlayer(player2);
        rocket2.ChangeRocketPos(new Vector2((int.Parse(WindowSize[0])) * 0.9f, (int.Parse(WindowSize[1]) * 0.1f)));
        totalBlackHole.Add(blackHole);
        blackHole.ChangePosition(new Vector2((int.Parse(WindowSize[0])) / 2, (int.Parse(WindowSize[1])) / 2));
        Settings.FirstRound = true;
    }

    public override void Draw()
    {
        base.Draw();
        rocket1.DrawRocket();
        rocket2.DrawRocket();
        blackHole.DrawBlackHole();
        //For all asteroids
        for (int i = 0; i <= totalAsteroid.Count - 1; i++)
        {
            totalAsteroid[i].DrawAsteroid();
        }
        //For all shoots
        for (int i = 0; i <= totalShot.Count - 1; i++)
        {
            totalShot[i].DrawShot();
        }

        //Draw if anyone wins
        if (winner != "not")
        {
            Text gameOver = new Text("Game over", new Vector2(Settings.Resolution.X / 2 - Settings.Resolution.X / 6, Settings.Resolution.Y / 3), 3f);
            gameOver.DrawText();
            if (winner == "draw")
            {
                Text Draw = new Text("Draw", new Vector2(Settings.Resolution.X / 2 - Settings.Resolution.X / 8, Settings.Resolution.Y / 2 + Settings.Resolution.Y / 6), 2.5f);
                Draw.DrawText();
                for (int i = 0; i <= rocket1.DestroyedGroup().Count - 1; i++)
                {
                    rocket1.DestroyedGroup()[i].DrawDestroyed();
                }
                for (int i = 0; i <= rocket2.DestroyedGroup().Count - 1; i++)
                {
                    rocket2.DestroyedGroup()[i].DrawDestroyed();
                }
            }

            if (winner == "player2")
            {
                Text player2 = new Text("Player 2 wins", new Vector2(Settings.Resolution.X / 2 - Settings.Resolution.X / 6, Settings.Resolution.Y / 2 + Settings.Resolution.Y / 6), 2.5f);
                player2.DrawText();
                rocket2.validMovementChange(false);
                for (int i = 0; i <= rocket1.DestroyedGroup().Count - 1; i++)
                {
                    rocket1.DestroyedGroup()[i].DrawDestroyed();
                }

            }
            if (winner == "player1")
            {
                Text player1 = new Text("Player 1 wins", new Vector2(Settings.Resolution.X / 2 - Settings.Resolution.X / 6, Settings.Resolution.Y / 2 + Settings.Resolution.Y / 6), 2.5f);
                player1.DrawText();
                rocket1.validMovementChange(false);
                for (int i = 0; i <= rocket2.DestroyedGroup().Count - 1; i++)
                {
                    rocket2.DestroyedGroup()[i].DrawDestroyed();
                }

            }
            Text controlls = new Text("Press enter to rematch", new Vector2(Settings.Resolution.X / 2f - Settings.Resolution.X / 5, Settings.Resolution.Y / 2 + Settings.Resolution.Y / 4), 1.5f);
            Text controlls2 = new Text("Press backspace to go back to start", new Vector2(Settings.Resolution.X / 3 - Settings.Resolution.X / 6, Settings.Resolution.Y / 2 + Settings.Resolution.Y / 3), 1.5f);
            controlls.DrawText();
            controlls2.DrawText();
        }
    }
    public override void Update()
    {
        base.Update();
        if (Settings.FirstRound == true)
        {
            //for the starting countdown
            begin();
        }
        if (Settings.FirstRound == false)
        {
            //Allow movement
            rocket1.Movement();
            rocket2.Movement();
            //Pulling towards a blackhole
            blackHole.Pull(rocket1);
            blackHole.Pull(rocket2);
            for (var i = 0; i < totalAsteroid.Count; i++)
            {
                totalAsteroid[i].AstreroidRun();
                for (var k = 0; k < totalBlackHole.Count; k++)
                {
                    totalBlackHole[k].Pull(totalAsteroid[i]);
                    // Koden blev problematisk och var inte viktig
                    // if (!(totalShot.Count == 0))
                    // {
                    //     totalBlackHole[k].Pull(totalShot[i]);
                    // }
                }
            }
            //if Asteroids should spawn
            if (Settings.Asteroids == true)
            {
                if (ticker % (Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) * 8) == 0)
                {
                    totalAsteroid.Add(new Asteroid());
                }
            }
            //when you can shoot
            if (rocket1.ShotCheck(ticker))
            {
                totalShot.Add(new Shot(rocket1.GetRocketPos() + new Vector2(rocket1.GetRocketHitbox().width / 2, rocket1.GetRocketHitbox().height / 2), rocket1.GetRocketSpeed(), rocket1.GetRocketRotaion(), "player1"));
            }
            if (rocket2.ShotCheck(ticker))
            {
                totalShot.Add(new Shot(rocket2.GetRocketPos() + new Vector2(rocket1.GetRocketHitbox().width / 2, rocket1.GetRocketHitbox().height / 2), rocket2.GetRocketSpeed(), rocket2.GetRocketRotaion(), "player2"));
            }
            for (int i = 0; i <= totalShot.Count - 1; i++)
            {
                totalShot[i].ShotRun();
            }
            //did someone die?
            if (winner == "not")
            {
                rocket1.CheckDeath(rocket2, totalShot, totalAsteroid, totalBlackHole);
                rocket2.CheckDeath(rocket1, totalShot, totalAsteroid, totalBlackHole);
            }
            //Who won
            if (winner == "not")
            {
                switch (rocket1.GetDeath())
                {
                    case true:
                        switch (rocket2.GetDeath())
                        {
                            case true:
                                winner = "not";
                                break;
                            case false:
                                winner = "player1";
                                break;
                        }
                        break;
                    case false:
                        switch (rocket2.GetDeath())
                        {
                            case true:
                                winner = "player2";
                                break;
                            case false:
                                winner = "draw";
                                break;
                        }
                        break;
                }
            }
            //Contorls if anyone wins
            if (winner != "not")
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
                {
                    Program.startingGame.group.AddScene(new Battle("player1", "player2"));
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_BACKSPACE))
                {
                    Program.startingGame.group.AddScene(new Start());
                }
            }
        }
    }
    public void begin()
    {
        if (ticker >= (Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) * 7))
        {
            Settings.FirstRound = false;
        }
        else if (ticker >= Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) * 5)
        {

            Raylib.DrawText("1", int.Parse(WindowSize[0]) / 2, int.Parse(WindowSize[1]) / 2, 100, Color.WHITE);
        }
        else if (ticker >= Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) * 3)
        {

            Raylib.DrawText("2", int.Parse(WindowSize[0]) / 2, int.Parse(WindowSize[1]) / 2, 100, Color.WHITE);
        }
        else if (ticker >= Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()))
        {

            Raylib.DrawText("3", int.Parse(WindowSize[0]) / 2, int.Parse(WindowSize[1]) / 2, 100, Color.WHITE);
        }
        ticker++;
    }
}
