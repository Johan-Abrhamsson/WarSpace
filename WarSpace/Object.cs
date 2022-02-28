using Raylib_cs;
using System;
using System.Numerics;
using System.IO;

abstract public class Object
{
    protected Vector2 accileration;
    protected Random generator = new Random();
    protected Vector2 position;

    protected float radius;

    protected Vector2 speed = new Vector2(0, 0);

    protected float angle;

    protected Vector2 rotation = new Vector2((float)Math.Cos(0), (float)Math.Sin(0));

    protected Color color;

    protected Rectangle hitBox;

    protected string[] WindowSize = File.ReadAllLines(@"resolution.txt");

    protected Vector2 textureValue;

    public Vector2 GetPosition()
    {
        return position;
    }

    public Vector2 ChangePosition(Vector2 newPosition)
    {
        this.position = newPosition;
        return position;
    }

    public float GetSize()
    {
        return radius;
    }

    public float GetAngle()
    {
        return angle;
    }

    public Vector2 GetRotation()
    {
        return rotation;
    }

    public Vector2 AppledForce(Force force)
    {
        speed += force.GetForce();
        //rotation += force.GetForce();
        return speed;
    }

    public void Drawing(int pictureValueX, int pictureValueY)
    {
        Raylib.DrawTexturePro(TextureCollection.other, new Raylib_cs.Rectangle(((pictureValueX - 1) * (TextureCollection.other.width / 3)), ((pictureValueY - 1) * (TextureCollection.other.height / 3)), TextureCollection.other.width / 3, TextureCollection.other.height / 3), new Raylib_cs.Rectangle(position.X, position.Y, radius * 2, radius * 2), new Vector2(radius, radius), (angle * 57.2957795131f) + 90, this.color);
    }
}