namespace ConsoleUI;

public struct ConsoleCharacter
{
    public char chr = ' ';
    // public ConsoleColor color = ConsoleColor.Black;
    // public ConsoleColor bgColor = ConsoleColor.Black;

    public ConColor foreColor;
    public ConColor bgColor;

    public ConsoleCharacter()
    {
    }

    public ConsoleCharacter(char _chr, ConsoleColor foreConsoleColor, ConsoleColor bgConsoleColor)
    {
        chr = _chr;
        foreColor = foreConsoleColor.ToRGB();
        bgColor = bgConsoleColor.ToRGB();
    }

    public ConsoleCharacter(char _cr, (int r, int g, int b) _fore, (int r, int g, int b) _bg)
    {
        chr = _cr;
        foreColor = _fore;
        bgColor = _bg;
    }

    public override string ToString()
    {
        return $"C:{chr} F:({foreColor.r},{foreColor.g},{foreColor.b}) B:({bgColor.r},{bgColor.g},{bgColor.b})";
    }

    public char[] ToBuffer()
    {
        var buffer = new char[43];
        ConsoleColorConverter.SetRGBBuffer(foreColor, bgColor, chr, ref buffer, 0);
        return buffer;
    }
}