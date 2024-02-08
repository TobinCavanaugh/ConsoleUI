namespace ConsoleUI;

public struct ConsoleCharacter
{
    public char chr = ' ';
    public ConsoleColor color = ConsoleColor.Black;
    public bool dirty = true;

    public ConsoleCharacter()
    {
    }

    public ConsoleCharacter(char _chr, ConsoleColor _color)
    {
        chr = _chr;
        color = _color;
    }
}