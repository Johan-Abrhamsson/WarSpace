using System.Net.WebSockets;
using System.Numerics;
using System;
using Raylib_cs;
using System.Collections.Generic;
public class Rocket
{
    Vector2 position;

    Vector2 speed = new Vector2(0, 0);

    Vector2 accileration = new Vector2(0.03f, 0.03f);

    double angle;

    Vector2 rotation = new Vector2((float)Math.Cos(0), (float)Math.Sin(0));

    string player;

    bool validMovement = true;

    int currentTicker;

    Rectangle hitbox = new Rectangle();

    Random generator = new Random();

    public Rocket(Vector2 positionNew, double angleNew, bool validMovementNew, string playerNew)
    {
        this.position = positionNew;
        this.angle = angleNew;
        this.rotation.X = (float)Math.Cos(angle);
        this.rotation.Y = (float)Math.Sin(angle);
        this.validMovement = validMovementNew;
        this.player = playerNew;
        this.hitbox = new Rectangle(position.X, position.Y, 10, 10);
    }

    public void Movement()
    {
        if (validMovement == true)
        {
            if (player == "player1")
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                {
                    speed += accileration;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                {
                    speed -= accileration;
                    if (speed.X <= 0 && speed.Y <= 0)
                    {
                        speed = new Vector2(0, 0);
                    }
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                {
                    angle += (double)accileration.X;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                {
                    angle -= (double)accileration.X;
                }
            }
            else if (player == "player2")
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    speed += accileration;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    speed -= accileration;
                    if (speed.X <= 0 && speed.Y <= 0)
                    {
                        speed = new Vector2(0, 0);
                    }
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    angle += (double)accileration.X;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    angle -= (double)accileration.X;
                }
            }
        }
        rotation.X = (float)Math.Cos(angle);
        rotation.Y = (float)Math.Sin(angle);
        position.X += speed.X * rotation.X;
        position.Y += speed.Y * rotation.Y;
        hitbox.x = position.X;
        hitbox.y = position.Y;
    }

    public bool ShotCheck(int ticker)
    {
        if (validMovement)
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

        return false;
    }
    public bool Control(bool value)
    {
        validMovement = value;
        return validMovement;
    }

    public void DrawRocket()
    {
        Raylib.DrawLine((int)position.X, (int)position.Y, (int)(position.X + rotation.X * 20), (int)(position.Y + rotation.Y * 20), Color.RED);
        Raylib.DrawLine((int)position.X, (int)position.Y - 5, (int)(position.X + speed.X * 5), (int)position.Y - 5, Color.GREEN);
        Raylib.DrawRectangle((int)hitbox.x, (int)hitbox.y, (int)hitbox.width, (int)hitbox.height, Color.BLUE);
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

    public Vector2 AppledForce(Force force)
    {
        speed += force.GetForce();
        return speed;
    }

    public int ChangeTicker(int time)
    {
        this.currentTicker = time;
        return currentTicker;
    }

    public void CheckDeath(Rocket rocket, List<Shot> bullets, List<Asteroid> asteroids, List<BlackHole> blackHoles)
    {
        if (rocket.position.X >= this.hitbox.x && rocket.position.X <= this.hitbox.x + this.hitbox.width || rocket.position.Y >= this.hitbox.y && rocket.position.Y <= this.hitbox.y - this.hitbox.height)
        {
            Death();
        }
        for (var i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].GetPosition().X + bullets[i].GetSize() >= this.hitbox.x && bullets[i].GetPosition().X - bullets[i].GetSize() <= this.hitbox.x + this.hitbox.width || bullets[i].GetPosition().Y - bullets[i].GetSize() >= this.hitbox.y && bullets[i].GetPosition().Y + bullets[i].GetSize() <= this.hitbox.y - this.hitbox.height)
            {
                Death();
            }
        }
        for (var i = 0; i < asteroids.Count; i++)
        {
            if (asteroids[i].GetPosition().X + asteroids[i].GetSize() >= this.hitbox.x && asteroids[i].GetPosition().X - asteroids[i].GetSize() <= this.hitbox.x + this.hitbox.width && asteroids[i].GetPosition().Y - asteroids[i].GetSize() >= this.hitbox.y && asteroids[i].GetPosition().Y + asteroids[i].GetSize() <= this.hitbox.y - this.hitbox.height)
            {
                Death();
            }
        }
        for (var i = 0; i < blackHoles.Count; i++)
        {
            if (blackHoles[i].GetPosition().X + blackHoles[i].GetSize() >= this.hitbox.x && blackHoles[i].GetPosition().X - blackHoles[i].GetSize() <= this.hitbox.x + this.hitbox.width && blackHoles[i].GetPosition().Y - blackHoles[i].GetSize() >= this.hitbox.y && bullets[i].GetPosition().Y + blackHoles[i].GetSize() <= this.hitbox.y - this.hitbox.height)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        validMovement = false;
        int parts;
        parts = generator.Next(3, 8);
        for (int i = 0; i <= parts; i++)
        {
            Destroyed part = new Destroyed(position, speed, rotation);
        }
        Console.WriteLine("Death");
    }
}