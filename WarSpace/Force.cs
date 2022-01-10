using System.Numerics;
using System;

public class Force
{
    float Strength;

    Vector2 rotation;

    Vector2 force;

    public Force(float newStrength, Vector2 rotation)
    {
        this.Strength = newStrength;
        this.rotation = rotation;
        force = rotation * new Vector2(Strength, Strength);
    }

    public Vector2 GetForce()
    {
        return this.force;
    }
}