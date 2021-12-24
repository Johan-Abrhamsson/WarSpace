using System;
using System.Collections.Generic;
using System.Numerics;

public class Battle : Scene
{
    List<Shot> totalShot = new List<Shot>();

    List<Asteroid> totalAsteroid = new List<Asteroid>();

    List<BlackHole> totalBlackHole = new List<BlackHole>();

    Rocket rocket1 = new Rocket(new Vector2(250, 250), 45, true, "-");

    Rocket rocket2 = new Rocket(new Vector2(300, 300), 275, true, "-");

    string player1;

    string player2;

    public Battle(string player1Value, string player2Value)
    {
        this.player1 = player1Value;
        this.player2 = player2Value;
        rocket1.ChangePlayer(player1);
        rocket2.ChangePlayer(player2);
    }

    public override void Draw()
    {
        base.Draw();
        rocket1.DrawRocket();
        rocket2.DrawRocket();
        for (int i = 0; i <= totalShot.Count - 1; i++)
        {
            totalShot[i].DrawShot();
        }
    }
    public override void Update()
    {
        base.Update();
        rocket1.Movement();
        rocket2.Movement();
        if (rocket1.ShotCheck(ticker))
        {
            totalShot.Add(new Shot(rocket1.GetRocketPos(), rocket1.GetRocketSpeed().X * new Vector2(2, 2) + new Vector2(0.5f, 0.5f), rocket1.GetRocketRotaion()));
        }
        if (rocket2.ShotCheck(ticker))
        {
            totalShot.Add(new Shot(rocket2.GetRocketPos(), rocket2.GetRocketSpeed() * new Vector2(2, 2) + new Vector2(0.5f, 0.5f), rocket2.GetRocketRotaion()));
        }
        for (int i = 0; i <= totalShot.Count - 1; i++)
        {
            totalShot[i].ShotRun();
        }
        //rocket1.CheckDeath(rocket2, totalShot, totalAsteroid, totalBlackHole);
        //rocket2.CheckDeath(rocket1, totalShot, totalAsteroid, totalBlackHole);
    }
}
