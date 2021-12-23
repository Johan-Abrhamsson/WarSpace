using System.Numerics;
using System;

public class Force
{
    float Strength;

    float angle;

    Vector2 force;

    public Force(float newStrength, float newAngle)
    {
        this.Strength = newStrength;
        this.angle = newAngle;
        force.X = (float)Math.Cos(angle) * Strength;
        force.Y = (float)Math.Sin(angle) * Strength;
    }

    public Vector2 GetForce()
    {
        return this.force;
    }
}