namespace ConsoleUI;

public struct ConColor
{
    public byte r;
    public byte g;
    public byte b;

    private void AssignColors(int _r, int _g, int _b)
    {
        r = (byte) Math.Clamp(_r, 0, 255);
        g = (byte) Math.Clamp(_g, 0, 255);
        b = (byte) Math.Clamp(_b, 0, 255);
    }

    public ConColor(int _r, int _g, int _b)
    {
        AssignColors(_r, _g, _b);
    }

    public ConColor(ConsoleColor color)
    {
        (int r, int g, int b) rgb = color.ToRGB();
        AssignColors(rgb.r, rgb.g, rgb.b);
    }

    public static implicit operator ConColor((int r, int g, int b) color)
    {
        return new ConColor(color.r, color.g, color.b);
    }

    public static implicit operator ConColor(ConsoleColor color)
    {
        return new ConColor(color);
    }
}