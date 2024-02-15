namespace ConsoleUI;

public struct ConsoleCharacter
{
    public char chr = ' ';
    // public ConsoleColor color = ConsoleColor.Black;
    // public ConsoleColor bgColor = ConsoleColor.Black;

    public (int r, int g, int b) foreColor;
    public (int r, int g, int b) bgColor;

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
        foreColor = _fore;
        bgColor = _bg;
    }
}