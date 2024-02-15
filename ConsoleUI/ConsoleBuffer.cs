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

    public static void Clear()
    {
        for (var i = 0; i < yLength; i++)
        {
            for (var i1 = 0; i1 < xLength; i1++)
            {
                screenBuffer[i, i1].chr = ' ';
                var cleared = ConsoleInterface.clearColor.ToRGB();
                screenBuffer[i, i1].bgColor = cleared;
                screenBuffer[i, i1].foreColor = cleared;
            }
        }
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

        var bgc = sample.bgColor;
        if (bgc.r < 0 && bgc.g < 0 && bgc.b < 0)
        {
            sample.bgColor = screenBuffer[y, x].bgColor;
            // sample.bgColor = ConsoleColor.Red.ToRGB();
        }

        var fcr = sample.foreColor;
        if (fcr.r < 0 && fcr.g < 0 && fcr.b < 0)
        {
            sample.foreColor = screenBuffer[y, x].foreColor;
        }

        screenBuffer[y, x] = sample;
    }

    public static void WriteAt(string data, Vec2 pos,
        ConsoleColor foreColor = ConsoleColor.White,
        ConsoleColor backColor = ConsoleColor.Black)
    {
        var cursorPos = ConsoleInterface.GetCursorPos();

        WriteAt(data, pos, foreColor.ToRGB(), backColor.ToRGB());
    }

    public static void WriteAt(string data, Vec2 pos,
        (int r, int g, int b) foreground,
        (int r, int g, int b) background)
    {
        var cursorPos = ConsoleInterface.GetCursorPos();

        int i = 0;
        for (int x = pos.x; x < pos.x + data.Length; x++)
        {
            pos.y = Math.Clamp(pos.y, 0, yLength);

            SafeSet(x, pos.y, new ConsoleCharacter()
            {
                chr = data[x - pos.x],
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

        ConsoleInterface.SetCursorPos(cursorPos);
    }

    public static void RenderScreenBuffer()
    {
        // Console.Clear();
        ConsoleInterface.CheckBufferSize();

        var pos = ConsoleInterface.GetCursorPos();

        Console.CursorVisible = false;
        ConsoleInterface.SetCursorPos(0, 0);

        var rgbBuffer = ConsoleColorConverter.GetColorCharBuffer();

        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                var current = screenBuffer[y, x];

                ConsoleColorConverter.SetRGBBuffer(current.bgColor, '\0', ref rgbBuffer, 48);
                Console.Write(rgbBuffer);

                ConsoleColorConverter.SetRGBBuffer(current.foreColor, current.chr, ref rgbBuffer, 38);
                Console.Write(rgbBuffer);
            }
        }

        // sw.Stop();
        // File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "/render.txt", sw.ElapsedMilliseconds + "ms,");

        ConsoleInterface.SetCursorPos(pos);
        Console.CursorVisible = true;
    }
}