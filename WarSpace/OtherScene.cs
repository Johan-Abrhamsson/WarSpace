using System.Globalization;
using System;
using System.Collections.Generic;
using System.Numerics;

public class OtherScene : Scene
{
    List<ClickBox> buttons;

    float distance;
    public OtherScene(List<ClickBox> newButtons)
    {
        this.buttons = newButtons;
        distance = (int.Parse(WindowSize[1]) * 0.75f) / buttons.Count;
        for (int i = 0; i <= buttons.Count - 1; i++)
        {
            Console.WriteLine(buttons[i].GetText());
            Console.WriteLine(buttons[i]);
            Console.WriteLine(i);
            buttons[i].ChangeClickBoxPos(new Vector2((int.Parse(WindowSize[0])) / 5, ((int.Parse(WindowSize[0])) * 0.25f) + i * distance));
        }
    }

    public override void Draw()
    {
        base.Draw();
        foreach (ClickBox C in buttons)
        {
            C.DrawClickBox(true);
        }
    }
    public override void Update()
    {
        base.Update();
    }
}