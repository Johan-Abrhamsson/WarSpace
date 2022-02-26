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

    protected Texture2D texture;

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

    public Texture2D GetTexture()
    {
        return this.texture;
    }

}