using System.Data.Common;

namespace ConsoleUI;

public class ConsoleBuffer
{
    public static ConsoleCharacter[,] screenBuffer;

    public static void RebuildBuffer()
    {
        var dim = ConsoleInterface.GetConsoleDimensions();
        screenBuffer = new ConsoleCharacter[dim.y, dim.x];

        for (int y = 0; y < dim.y; y++)
        {
            for (int x = 0; x < dim.x; x++)
            {
                screenBuffer[y, x] = new ConsoleCharacter();
            }
        }
    }

    public static void Clear()
    {
        for (var i = 0; i < screenBuffer.GetLength(0); i++)
        {
            for (var i1 = 0; i1 < screenBuffer.GetLength(1); i1++)
            {
                screenBuffer[i, i1].chr = ' ';
                screenBuffer[i, i1].color = ConsoleInterface.clearColor;
                screenBuffer[i, i1].dirty = true;
            }
        }
    }

    public static void MarkRegionDirty(Vec2 upperLeft, Vec2 bottomRight)
    {
        var my = screenBuffer.GetLength(0);
        var mx = screenBuffer.GetLength(1);

        for (int y = Math.Clamp(upperLeft.y, 0, my); y < Math.Clamp(bottomRight.y, 0, my - 1); y++)
        {
            for (int x = Math.Clamp(upperLeft.x, 0, mx); y < Math.Clamp(bottomRight.x, 0, mx - 1); x++)
            {
                screenBuffer[x, y].dirty = true;
            }
        }
    }


    public static void FillRegion(Vec2 upperLeft, Vec2 bottomRight, ConsoleCharacter sample)
    {
        var dim = ConsoleInterface.GetConsoleDimensions();
    }

    public static void SafeSet(int x, int y, ConsoleCharacter sample)
    {
        var my = screenBuffer.GetLength(0);
        var mx = screenBuffer.GetLength(1);

        if (mx == 0 && my == 0)
        {
            return;
        }

        if (x >= mx || x < 0 || y >= my || y < 0)
        {
            return;
        }

        // var dim = ConsoleInterface.GetConsoleDimensions();
        // x = Math.Clamp(x, 0, Math.Min(mx, dim.x));
        // y = Math.Clamp(y, 0, Math.Min(my, dim.y));

        screenBuffer[y, x] = sample;
    }

    public static void WriteAt(string data, Vec2 pos, ConsoleColor color = ConsoleColor.White)
    {
        var cursorPos = ConsoleInterface.GetCursorPos();
        var dim = ConsoleInterface.GetConsoleDimensions();

        pos = ConsoleInterface.ClampToConsole(pos);

        try
        {
            int i = 0;
            Console.ForegroundColor = color;
            for (int x = pos.x; x < pos.x + data.Length; x++)
            {
                pos.y = Math.Clamp(pos.y, 0, screenBuffer.GetLength(0));
                screenBuffer[pos.y, x].chr = data[x - pos.x];
                screenBuffer[pos.y, x].color = color;
                screenBuffer[pos.y, x].dirty = true;
                i++;
            }
        }
        catch (Exception ex)
        {
        }
        //
        // Parallel.For(pos.x, Math.Min(pos.x + data.Length, dim.x), x =>
        // {
        //     if (screenBuffer.GetLength(0) > pos.y || screenBuffer.GetLength(1) > x)
        //     {
        //         screenBuffer[pos.y, x].chr = data[x - pos.x];
        //         screenBuffer[pos.y, x].color = color;
        //         screenBuffer[pos.y, x].dirty = true;
        //     }
        // });

        ConsoleInterface.SetCursorPos(cursorPos);
    }

    public static void RenderScreenBuffer()
    {
        ConsoleInterface.CheckBufferSize();

        var pos = ConsoleInterface.GetCursorPos();
        ConsoleInterface.SetCursorPos(0, 0);

        for (int y = 0; y < screenBuffer.GetLength(0); y++)
        {
            for (int x = 0; x < screenBuffer.GetLength(1); x++)
            {
                if (screenBuffer[y, x].dirty)
                {
                    ConsoleInterface.SetCursorPos(x, y);
                    Console.Write(screenBuffer[y, x].chr + "", screenBuffer[y, x].color);
                    screenBuffer[y, x].dirty = false;
                }
            }
        }

        ConsoleInterface.SetCursorPos(pos);
    }
}