using System;
using System.Numerics;
using Raylib_cs;
public class Asteroid : Object
{
    int textureChoise;
    public Asteroid()
    {
        this.position = new Vector2(generator.Next(-100, (int.Parse(WindowSize[0])) + 100), generator.Next(-100, (int.Parse(WindowSize[1]) + 100)));
        while (position.X >= -10 && position.X <= (int.Parse(WindowSize[0])) + 10 && position.Y >= -10 && position.Y <= (int.Parse(WindowSize[1])) + 10)
        {
            this.position = new Vector2(generator.Next(-100, (int.Parse(WindowSize[0])) + 100), generator.Next(-100, (int.Parse(WindowSize[1]) + 100)));
        }
        this.radius = generator.Next(10, 20);
        this.color = Color.RED;
        this.textureChoise = generator.Next(1, 4);
        this.angle = generator.Next(0, (int)(2 * Math.PI));
        switch (this.textureChoise)
        {
            case 1:
                this.textureValue = new Vector2(1, 1);
                break;
            case 2:
                this.textureValue = new Vector2(2, 1);
                break;
            case 3:
                this.textureValue = new Vector2(3, 1);
                break;
            case 4:
                this.textureValue = new Vector2(1, 2);
                break;
        }
    }

    public void AstreroidRun()
    {
        rotation.X = (float)Math.Cos(angle);
        rotation.Y = (float)Math.Sin(angle);
        position.X += speed.X;
        position.Y += speed.Y;
        hitBox.x = position.X;
        hitBox.y = position.Y;
    }

    public void DrawAsteroid()
    {
        //Raylib.DrawCircleV(position, radius, color);
        this.Drawing((int)textureValue.X, (int)textureValue.Y);
    }
}