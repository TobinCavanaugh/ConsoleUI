using System.Drawing;

namespace ConsoleUI;

public static class ConsoleInterface
{
    public static ConsoleColor clearColor = ConsoleColor.Black;

    public static void Init()
    {
        BufferDimensions = GetConsoleDimensionsNew();
    }

    private static Vec2 GetConsoleDimensionsNew()
    {
        var dim = (Console.BufferWidth, Console.BufferHeight);
        BufferDimensions = dim;
        return dim;
    }

    public static Vec2 GetConsoleDimensions()
    {
        return BufferDimensions;
    }

    public static Vec2 ClampToConsole(Vec2 pre)
    {
        var dim = GetConsoleDimensions();
        return (Math.Clamp(pre.x, 0, dim.x), Math.Clamp(pre.y, 0, dim.y));
    }

    public static void MoveCursor(Vec2 add)
    {
        MoveCursor(add.x, add.y);
    }

    public static void MoveCursor(int x, int y)
    {
        (int left, int top) = Console.GetCursorPosition();
        SetCursorPos(left + x, top + y);
    }

    public static void SetCursorPos(Vec2 pos)
    {
        SetCursorPos(pos.x, pos.y);
    }

    public static void SetCursorPos(int x, int y)
    {
        x = Math.Clamp(x, 0, Console.BufferWidth - 1);
        y = Math.Clamp(y, 0, Console.BufferHeight - 1);

        try
        {
            Console.SetCursorPosition(x, y);
        }
        catch (Exception ex)
        {
        }
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


    public static void Clear()
    {
        var pos = GetCursorPos();

        // Console.MoveBufferArea(0, 0, Console.BufferWidth, Console.BufferHeight, 0, 0);
        // Console.WindowHeight = Console.BufferHeight;
        // Console.WindowWidth = Console.BufferWidth;

        Console.Clear();

        Console.SetCursorPosition(pos.x, pos.y);
    }

    public static Vec2 GetMousePos()
    {
        Point p = new Point();
        WindowsInterface.GetCursorPos(ref p);
        return (p.X, p.Y);
    }

    private static Vec2 BufferDimensions;

    public static void CheckBufferSize()
    {
        SetCursorPos(GetCursorPos());

        var oldDim = GetConsoleDimensions();
        var dim = GetConsoleDimensionsNew();
        if (!oldDim.Equals(dim))
        {
            ConsoleBuffer.RebuildBuffer();
            BufferDimensions = dim;
        }
    }
}