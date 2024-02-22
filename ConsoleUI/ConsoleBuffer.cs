namespace ConsoleUI;

public static class ConsoleBuffer
{
    public static ConsoleCharacter[,] screenBuffer;

    private static int yLength = 0;
    private static int xLength = 0;

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

        xLength = dim.x;
        yLength = dim.y;
    }

    public static void FillRegion(Vec2 upperLeft, Vec2 bottomRight, ConsoleCharacter sample)
    {
        var dim = ConsoleInterface.GetConsoleDimensions();

        for (int y = upperLeft.y; y < bottomRight.y; y++)
        {
            for (int x = upperLeft.x; x < bottomRight.x; x++)
            {
                SafeSet(x, y, sample);
            }
        }
    }

    public static void SafeSet(int x, int y, ConsoleCharacter sample)
    {
        var my = yLength;
        var mx = xLength;

        if (mx == 0 && my == 0)
        {
            return;
        }

        if (x >= mx || x < 0 || y >= my || y < 0)
        {
            return;
        }

        // var bgc = sample.bgColor;
        // if (bgc.r < 0 && bgc.g < 0 && bgc.b < 0)
        // {
        // sample.bgColor = screenBuffer[y, x].bgColor;
        // sample.bgColor = ConsoleColor.Red.ToRGB();
        // }

        // var fcr = sample.foreColor;
        // if (fcr.r < 0 && fcr.g < 0 && fcr.b < 0)
        // {
        // sample.foreColor = screenBuffer[y, x].foreColor;
        // }

        screenBuffer[y, x] = sample;
    }

    public static void WriteAt(string data, Vec2 pos,
        ConsoleColor foreColor = ConsoleColor.White,
        ConsoleColor backColor = ConsoleColor.Black)
    {
        WriteAt(data, pos, foreColor.ToRGB(), backColor.ToRGB());
    }

    public static void WriteAt(string data, Vec2 pos,
        ConColor foreground,
        ConColor background)
    {
        for (int x = 0; x < data.Length; x++)
        {
            SafeSet(x + pos.x, pos.y, new ConsoleCharacter()
            {
                chr = data[x],
                bgColor = background,
                foreColor = foreground
            });
        }
        //
        // Parallel.For(pos.x, Math.Min(pos.x + data.Length, dim.x), x =>
        // {
        //     if (screenBuffer.GetLength(0) > pos.y || xLength) > x)
        //     {
        //         screenBuffer[pos.y, x].chr = data[x - pos.x];
        //         screenBuffer[pos.y, x].foreColor = foreColor;
        //         screenBuffer[pos.y, x].dirty = true;
        //     }
        // });
    }

    public static void RenderScreenBuffer()
    {
        ConsoleInterface.CheckBufferSize();

        var pos = ConsoleInterface.GetCursorPos();

        var visible = Console.CursorVisible;
        Console.CursorVisible = false;

        ConsoleInterface.SetCursorPos(0, 0);

        var rgbBuffer = ConsoleColorConverter.GetEmptyColorCharBuffer(xLength + 1);

        bool colored = true;

        for (int y = 0; y < yLength - 1; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                var current = screenBuffer[y, x];

                if (colored)
                {
                    ConsoleColorConverter.SetRGBBuffer(
                        current.foreColor, current.bgColor,
                        current.chr, ref rgbBuffer,
                        x * ConsoleColorConverter.GetColorBufferLength());
                }
                else
                {
                    rgbBuffer[x] = current.chr;
                }
            }

            Console.Write(rgbBuffer);

            ConsoleInterface.SetCursorPos(0, y + 1);
        }

        ConsoleInterface.SetCursorPos(pos);
        Console.CursorVisible = visible;
    }
}