using System;
using System.Drawing;

public class SkyLayer : BaseLayer
{
    DateTime start = DateTime.Now;



    public override void Draw(Graphics g)
    {
        var time = DateTime.Now - start;
        var secs = (float)time.TotalSeconds;
        var cycleDuration  = 400f; 
        var factor = (secs % cycleDuration) / cycleDuration ;

        Color[] colors = new Color[]
        {
            Color.FromArgb(135, 206, 250),
            Color.FromArgb(24, 157, 240),
            Color.FromArgb(245, 147, 27),
            Color.FromArgb(2, 2, 46),
            Color.FromArgb(135, 206, 250)

        };


        int index = (int)(factor * (colors.Length - 1));
        float localFactor = factor * (colors.Length - 1) - index;

        Color startColor = colors[index];
        Color endColor = colors[index + 1];
        int r = (int)Calc(startColor.R, endColor.R, localFactor);
        int gg = (int)Calc(startColor.G, endColor.G, localFactor);
        int b = (int)Calc(startColor.B, endColor.B, localFactor);

        g.Clear(Color.FromArgb(r, gg, b));
    }

    private float Calc(float start, float end, float factor)
    {
        return start + (end - start) * factor;
    }

    private Color hsvToColor(int h, int s, int v)
    {
        h = Math.Max(0, Math.Min(360, h));
        s = Math.Max(0, Math.Min(255, s));
        v = Math.Max(0, Math.Min(255, v));

        int r = 0,
            g = 0,
            b = 0;

        if (s == 0)
        {
            r = v;
            g = v;
            b = v;
            return Color.FromArgb(LimitColor(r), LimitColor(g), LimitColor(b));
        }

        int region = h / 43;
        int remainder = (h - (region * 43)) * 6;

        int p = (v * (255 - s)) >> 8;
        int q = (v * (255 - ((s * remainder) >> 8))) >> 8;
        int t = (v * (255 - ((s * (255 - remainder)) >> 8))) >> 8;

        switch (region)
        {
            case 0:
                r = v;
                g = t;
                b = p;
                break;
            case 1:
                r = q;
                g = v;
                b = p;
                break;
            case 2:
                r = p;
                g = v;
                b = t;
                break;
            case 3:
                r = p;
                g = q;
                b = v;
                break;
            case 4:
                r = t;
                g = p;
                b = v;
                break;
            default:
                r = v;
                g = p;
                b = q;
                break;
        }


        return Color.FromArgb(LimitColor(r), LimitColor(g), LimitColor(b));
    }


    private byte LimitColor(int value)
    {
        return (byte)Math.Max(0, Math.Min(255, value));
    }
}
