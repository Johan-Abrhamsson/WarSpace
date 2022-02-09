using System.Net.WebSockets;
using System.Numerics;
using System;
using Raylib_cs;
using System.Collections.Generic;
using System.IO;
public class Rocket : Object
{

    string player;

    bool validMovement = true;

    int currentTicker;

    bool isAlive = true;

    List<Destroyed> deathShow = new List<Destroyed>();

    public Rocket(Vector2 positionNew, float angleNew, bool validMovementNew, string playerNew)
    {
        this.position = positionNew;
        this.angle = angleNew;
        this.rotation.X = (float)Math.Cos(angle);
        this.rotation.Y = (float)Math.Sin(angle);
        this.validMovement = validMovementNew;
        this.player = playerNew;
        this.hitBox = new Rectangle(position.X, position.Y, 10, 10);
        accileration = new Vector2(0.05f, 0.05f);
    }

    public void Movement()
    {
        if (validMovement == true)
        {
            if (player == "player1")
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                {
                    AppledForce(new Force(accileration.X, rotation));
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                {
                    AppledForce(new Force(-accileration.X / 3, rotation));
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                {
                    angle -= accileration.X;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                {
                    angle += accileration.X;
                }
            }
            else if (player == "player2")
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    AppledForce(new Force(accileration.X, rotation));
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    AppledForce(new Force(-accileration.X / 3, rotation));
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    angle -= accileration.X;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    angle += accileration.X;
                }
            }
        }
        rotation.X = (float)Math.Cos(angle);
        rotation.Y = (float)Math.Sin(angle);
        position.X += speed.X;
        position.Y += speed.Y;
        hitBox.x = position.X;
        hitBox.y = position.Y;
    }

    public bool ShotCheck(int ticker)
    {
        if (validMovement)
        {
            if (!Settings.Lazer)
            {
                if (currentTicker <= ticker - Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) / 2)
                {
                    if (player == "player1")
                    {
                        if (Raylib.IsKeyDown(KeyboardKey.KEY_PERIOD))
                        {
                            currentTicker = ticker;
                            return true;
                        }
                    }
                    else if (player == "player2")
                    {
                        if (Raylib.IsKeyDown(KeyboardKey.KEY_G))
                        {
                            currentTicker = ticker;
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (player == "player1")
                {
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_PERIOD))
                    {
                        currentTicker = ticker;
                        return true;
                    }
                }
                else if (player == "player2")
                {
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_G))
                    {
                        currentTicker = ticker;
                        return true;
                    }
                }
            }
        }

        return false;
    }
    public bool Control(bool value)
    {
        validMovement = value;
        return validMovement;
    }

    public void DrawRocket()
    {
        if (player == "player1")
        {
            Raylib.DrawRectangle((int)hitBox.x, (int)hitBox.y, (int)hitBox.width, (int)hitBox.height, Color.BLUE);
            Raylib.DrawLine((int)position.X + (int)hitBox.width / 2, (int)position.Y + (int)hitBox.height / 2, (int)(position.X + rotation.X * 20) + (int)hitBox.width / 2, (int)(position.Y + rotation.Y * 20) + (int)hitBox.height / 2, Color.ORANGE);
        }
        else if (player == "player2")
        {
            Raylib.DrawRectangle((int)hitBox.x, (int)hitBox.y, (int)hitBox.width, (int)hitBox.height, Color.RED);
            Raylib.DrawLine((int)position.X + (int)hitBox.width / 2, (int)position.Y + (int)hitBox.height / 2, (int)(position.X + rotation.X * 20) + (int)hitBox.width / 2, (int)(position.Y + rotation.Y * 20) + (int)hitBox.height / 2, Color.YELLOW);
        }
        else
        {
            Raylib.DrawRectangle((int)hitBox.x, (int)hitBox.y, (int)hitBox.width, (int)hitBox.height, Color.RED);
            Raylib.DrawLine((int)position.X + (int)hitBox.width / 2, (int)position.Y + (int)hitBox.height / 2, (int)(position.X + rotation.X * 20) + (int)hitBox.width / 2, (int)(position.Y + rotation.Y * 20) + (int)hitBox.height / 2, Color.YELLOW);
        }
        Raylib.DrawLine((int)position.X + (int)hitBox.width / 2, (int)position.Y - 5, (int)(position.X + speed.X * 5) + (int)hitBox.width / 2, (int)position.Y - 5, Color.GREEN);
        Raylib.DrawLine((int)position.X + (int)hitBox.width / 2, (int)position.Y - 5, (int)position.X + (int)hitBox.width / 2, (int)(position.Y - 5 + speed.Y * 5), Color.PURPLE);
    }


    public Vector2 GetRocketPos()
    {
        return position;
    }
    public Vector2 GetRocketSpeed()
    {
        return speed;
    }
    public Vector2 GetRocketRotaion()
    {
        return rotation;
    }
    public Rectangle GetRocketHitbox()
    {
        return hitBox;
    }
    public Vector2 ChangeRocketPos(Vector2 Change)
    {
        this.position = Change;
        return position;
    }
    public string ChangePlayer(string playerName)
    {
        this.player = playerName;
        return player;
    }

    public int ChangeTicker(int time)
    {
        this.currentTicker = time;
        return currentTicker;
    }

    public void CheckDeath(Rocket rocket, List<Shot> bullets, List<Asteroid> asteroids, List<BlackHole> blackHoles)
    {
        if (rocket.position.X >= this.hitBox.x && rocket.position.X <= this.hitBox.x + this.hitBox.width && rocket.position.Y >= this.hitBox.y && rocket.position.Y <= this.hitBox.y + this.hitBox.height)
        {
            Death();
        }
        for (var i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].GetPosition().X + bullets[i].GetSize() >= this.hitBox.x && bullets[i].GetPosition().X - bullets[i].GetSize() <= this.hitBox.x + this.hitBox.width && bullets[i].GetPosition().Y + bullets[i].GetSize() >= this.hitBox.y && bullets[i].GetPosition().Y - bullets[i].GetSize() <= this.hitBox.y + this.hitBox.height)
            {
                if (bullets[i].GetPolarity() != player)
                {
                    Death();
                }
            }
        }
        for (var i = 0; i < asteroids.Count; i++)
        {
            if (asteroids[i].GetPosition().X + asteroids[i].GetSize() >= this.hitBox.x && asteroids[i].GetPosition().X - asteroids[i].GetSize() <= this.hitBox.x + this.hitBox.width && asteroids[i].GetPosition().Y + asteroids[i].GetSize() >= this.hitBox.y && asteroids[i].GetPosition().Y - asteroids[i].GetSize() <= this.hitBox.y + this.hitBox.height)
            {
                Death();
            }
        }
        for (var i = 0; i < blackHoles.Count - 1; i++)
        {
            if (blackHoles[i].GetPosition().X + blackHoles[i].GetSize() >= this.hitBox.x && blackHoles[i].GetPosition().X - blackHoles[i].GetSize() <= this.hitBox.x + this.hitBox.width && blackHoles[i].GetPosition().Y + blackHoles[i].GetSize() >= this.hitBox.y && bullets[i].GetPosition().Y - blackHoles[i].GetSize() <= this.hitBox.y + this.hitBox.height)
            {
                Death();
            }
        }
        if (this.hitBox.x + this.hitBox.width > int.Parse(WindowSize[0]) || this.hitBox.x < 0 || this.hitBox.y + this.hitBox.height > int.Parse(WindowSize[1]) || this.hitBox.y < 0)
        {
            Death();
        }
    }

    public void Death()
    {
        validMovement = false;
        isAlive = false;
        int parts;
        parts = generator.Next(3, 20);
        for (int i = 0; i <= parts; i++)
        {
            Destroyed part = new Destroyed(position, speed, rotation);
            deathShow.Add(part);
        }
        for (int i = 0; i <= generator.Next(10, 40); i++)
        {
            Raylib.DrawCircleV(position + new Vector2(hitBox.width / 2, hitBox.height / 2) + new Vector2(generator.Next(-20, 20), generator.Next(-20, 20)), generator.Next(1, 9), new Color(generator.Next(130, 255), generator.Next(0, 255), generator.Next(0, 90), generator.Next(1, 255)));
        }
    }

    public List<Destroyed> DestroyedGroup()
    {
        return deathShow;
    }

    public bool GetDeath()
    {
        return isAlive;
    }

    public bool validMovementChange(bool change)
    {
        this.validMovement = change;
        return validMovement;
    }
}