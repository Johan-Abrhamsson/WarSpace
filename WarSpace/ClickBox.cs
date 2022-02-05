using System.Threading;
using Raylib_cs;
using System.Numerics;
using System;
using System.IO;

public class ClickBox
{
    private Vector2 position;
    private Vector2 Size;

    private string text;

    private Color variant;

    private Color hover;

    private Color hoverDark;

    private byte value = 8;

    private bool mouseIsHover;

    private bool stillChecked;

    protected string[] WindowSize = File.ReadAllLines(@"resolution.txt");
    public ClickBox(string s, int x, int y, int widthValue, int heightValue, Color newVariant, Color hoverVariant)
    {
        text = s;
        position.X = x;
        position.Y = y;
        Size.X = widthValue;
        Size.Y = heightValue;
        hover = hoverVariant;
        hoverDark.r = (byte)(hover.r - value);
        hoverDark.g = (byte)(hover.g - value);
        hoverDark.b = (byte)(hover.b - value);
        variant = newVariant;
    }
    public bool Check(float mouseX, float mouseY)
    {
        // If the player stopps trying to click then the clickbox is not pressed
        if (Raylib.IsMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON))
        {
            this.stillChecked = false;
        }
        // If the courser is outside the bounding box using AABB collision detection, return false
        if ((mouseX < position.X) || (mouseX > position.X + Size.X) || (mouseY < position.Y) || (mouseY > position.Y + Size.Y))
        {
            return false;
        }

        // If the courser is inside the bounding box and is pressed, return true
        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
        {
            //However if it is not already pressed it should still run, otherwice if it is already pressed, do not run the code
            if (stillChecked == false)
            {
                this.stillChecked = true;
                return true;
            }
        }

        return false;
    }

    public void DrawClickBox(bool withText)
    {
        if (mouseIsHover == false)
        {
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)position.X + (int)Size.X, (int)position.Y, variant);
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)position.X, (int)position.Y + (int)Size.Y, variant);
            Raylib.DrawLine((int)position.X, (int)position.Y + (int)Size.Y, (int)position.X + (int)Size.X, (int)position.Y + (int)Size.Y, variant);
            Raylib.DrawLine((int)position.X + (int)Size.X, (int)position.Y, (int)position.X + (int)Size.X, (int)position.Y + (int)Size.Y, variant);

            if (withText == true)
            {
                Raylib.DrawText(text, (int)position.X + 1, ((int)position.Y + (int)Size.Y / 3), (int)Size.X / 7, variant);
            }
        }
        else
        {
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)position.X + (int)Size.X, (int)position.Y, hover);
            Raylib.DrawLine((int)position.X, (int)position.Y, (int)position.X, (int)position.Y + (int)Size.Y, hover);
            Raylib.DrawLine((int)position.X, (int)position.Y + (int)Size.Y, (int)position.X + (int)Size.X, (int)position.Y + (int)Size.Y, hover);
            Raylib.DrawLine((int)position.X + (int)Size.X, (int)position.Y, (int)position.X + (int)Size.X, (int)position.Y + (int)Size.Y, hover);

            if (withText == true)
            {
                Raylib.DrawText(text, (int)position.X + 3, ((int)position.Y + (int)Size.Y / 3), (int)Size.X / 6, Color.DARKGRAY);
                Raylib.DrawText(text, (int)position.X + 1, ((int)position.Y + (int)Size.Y / 3), (int)Size.X / 6, hover);
            }
        }
    }

    public void ClickBoxColorHover(float mouseX, float mouseY)
    {
        if (position.X <= mouseX && position.X + Size.X >= mouseX && position.Y <= mouseY && position.Y + Size.Y >= mouseY)
        {
            mouseIsHover = true;
        }
        else
        {
            mouseIsHover = false;
        }
    }


    public Vector2 GetPosition()
    {
        return position;
    }

    public Vector2 GetSize()
    {
        return Size;
    }

    public string GetText()
    {
        return text;
    }
    public Vector2 ChangeClickBoxPos(Vector2 Change)
    {
        this.position = Change;
        return position;
    }

    public Vector2 ChangeClickBoxSize(Vector2 Change)
    {
        this.Size = Change;
        return Size;
    }
}
