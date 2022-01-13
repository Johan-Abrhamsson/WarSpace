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

    BlackHole waddup = new BlackHole(new Vector2(600, 600), 20);

    string player1;

    string player2;

    string winner = "not";

    public Battle(string player1Value, string player2Value)
    {
        this.player1 = player1Value;
        this.player2 = player2Value;
        rocket1.ChangePlayer(player1);
        rocket1.ChangeRocketPos(new Vector2((int.Parse(WindowSize[0])) * 0.1f, (int.Parse(WindowSize[1]) * 0.9f)));
        rocket2.ChangePlayer(player2);
        rocket2.ChangeRocketPos(new Vector2((int.Parse(WindowSize[0])) * 0.9f, (int.Parse(WindowSize[1]) * 0.1f)));
        totalBlackHole.Add(waddup);
        waddup.ChangePosition(new Vector2((int.Parse(WindowSize[0])) / 2, (int.Parse(WindowSize[1])) / 2));
        Settings.FirstRound = true;
    }

    public override void Draw()
    {
        base.Draw();
        rocket1.DrawRocket();
        rocket2.DrawRocket();
        waddup.DrawBlackHole();
        for (int i = 0; i <= totalAsteroid.Count - 1; i++)
        {
            totalAsteroid[i].DrawAsteroid();
        }
        for (int i = 0; i <= totalShot.Count - 1; i++)
        {
            totalShot[i].DrawShot();
        }
        if (winner != "not")
        {
            Raylib.DrawText("Game over", 120, 120, 50, Color.WHITE);
            if (winner == "draw")
            {
                Raylib.DrawText("Draw", 120, 240, 50, Color.WHITE);
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
                Raylib.DrawText("Player 2 (wads) wins", 120, 240, 50, Color.WHITE);
                rocket2.validMovementChange(false);
                for (int i = 0; i <= rocket1.DestroyedGroup().Count - 1; i++)
                {
                    rocket1.DestroyedGroup()[i].DrawDestroyed();
                }

            }
            if (winner == "player1")
            {
                Raylib.DrawText("Player 1 (^v<>) wins", 120, 240, 50, Color.WHITE);
                rocket1.validMovementChange(false);
                for (int i = 0; i <= rocket2.DestroyedGroup().Count - 1; i++)
                {
                    rocket2.DestroyedGroup()[i].DrawDestroyed();
                }

            }
        }
    }
    public override void Update()
    {
        base.Update();
        if (Settings.FirstRound == true)
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
        if (Settings.FirstRound == false)
        {
            rocket1.Movement();
            rocket2.Movement();
            waddup.Pull(rocket1);
            waddup.Pull(rocket2);
            for (var i = 0; i < totalAsteroid.Count; i++)
            {
                totalAsteroid[i].AstreroidRun();
                for (var k = 0; k < totalBlackHole.Count; k++)
                {
                    totalBlackHole[k].Pull(totalAsteroid[i]);
                    if (!(totalShot.Count == 0))
                    {
                        totalBlackHole[k].Pull(totalShot[i]);
                    }
                }
            }
            if (Settings.Asteroids == true)
            {
                if (ticker % (Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) * 8) == 0)
                {
                    totalAsteroid.Add(new Asteroid());
                }
            }
            if (rocket1.ShotCheck(ticker))
            {
                totalShot.Add(new Shot(rocket1.GetRocketPos() + new Vector2(rocket1.GetRocketHitbox().width / 2, rocket1.GetRocketHitbox().height / 2), rocket1.GetRocketSpeed() + rocket1.GetRocketSpeed(), rocket1.GetRocketRotaion(), "player1"));
            }
            if (rocket2.ShotCheck(ticker))
            {
                totalShot.Add(new Shot(rocket2.GetRocketPos() + new Vector2(rocket1.GetRocketHitbox().width / 2, rocket1.GetRocketHitbox().height / 2), rocket2.GetRocketSpeed() + rocket2.GetRocketSpeed(), rocket2.GetRocketRotaion(), "player2"));
            }
            for (int i = 0; i <= totalShot.Count - 1; i++)
            {
                totalShot[i].ShotRun();
            }
            rocket1.CheckDeath(rocket2, totalShot, totalAsteroid, totalBlackHole);
            rocket2.CheckDeath(rocket1, totalShot, totalAsteroid, totalBlackHole);

            if (winner == "not")
            {
                if (!rocket1.GetDeath() && !rocket2.GetDeath())
                {
                    winner = "draw";
                }
                else
                {
                    if (!rocket1.GetDeath())
                    {
                        winner = "player2";
                    }
                    if (!rocket2.GetDeath())
                    {
                        winner = "player1";
                    }
                }
            }
            if (winner != "not")
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
                {
                    Program.startingGame.group.AddScene(new Battle("player1", "player2"));
                }
            }
        }
    }
}
