using System.Text;

namespace ConsoleUI;

public static class ConsoleInterface
{

    private static Stream stream;
    public static void Init()
    {
        stream = Console.OpenStandardOutput();
    }

    public static Vec2 GetConsoleDimensions()
    {
        return new Vec2(Console.BufferWidth, Console.BufferHeight);
    }

    public static void MoveCursor(Vec2 add)
    {
        MoveCursor(add.x, add.y);
    }

    public static void MoveCursor(int x, int y)
    {
        (int left, int top) = Console.GetCursorPosition();
        int newX = left + x;
        int newY = top + y;

        newX = Math.Clamp(newX, 0, Console.BufferWidth - 1);
        newY = Math.Clamp(newY, 0, Console.BufferHeight - 1);

        Console.SetCursorPosition(newX, newY);
    }

    public static void SetCursorPos(Vec2 pos)
    {
        SetCursorPos(pos.x, pos.y);
    }

    public static void SetCursorPos(int x, int y)
    {
        x = Math.Clamp(x, 0, Console.BufferWidth - 1);
        y = Math.Clamp(y, 0, Console.BufferHeight - 1);

        Console.SetCursorPosition(x, y);
    }

    public static Vec2 GetCursorPos()
    {
        return (Vec2) Console.GetCursorPosition();
    }

    public static int GetCursorPosX()
    {
        return GetCursorPos().x;
    }

    public static int GetCursorPosY()
    {
        return GetCursorPos().y;
    }


    public static void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
    }

    public static void WriteColoredAt(string text, ConsoleColor color, Vec2 pos)
    {
        var oldPos = GetCursorPos();
        SetCursorPos(pos);
        WriteColored(text, color);
        SetCursorPos(oldPos);
        
        // Console.ForegroundColor = color;
        // var dim = GetConsoleDimensions();
        // stream.Position = (dim.x * pos.y) + dim.x;
        // stream.Write(Encoding.UTF8.GetBytes(text));
    }

    public static void Clear()
    {
        var pos = GetCursorPos();
        
        Console.Clear();
        
        Console.SetCursorPosition(pos.x, pos.y);
    }
}