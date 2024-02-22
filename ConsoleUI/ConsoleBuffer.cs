namespace ConsoleUI;

public static class ConsoleBuffer
{
    public static ConsoleCharacter[,] screenBuffer;
    public static string[] previousHashes;

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

        previousHashes = new string[yLength];
    }

    public static void Clear()
    {
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                screenBuffer[y, x].foreColor = ConsoleInterface.clearColor;
                screenBuffer[y, x].bgColor = ConsoleInterface.clearColor;
                screenBuffer[y, x].chr = ' ';
            }
        }
    }

    /// <summary>
    /// Fills the region between upper left and bottom right with the sample
    /// </summary>
    /// <param name="upperLeft"></param>
    /// <param name="bottomRight"></param>
    /// <param name="sample"></param>
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

    /// <summary>
    /// Function to set a screenbuffer cell safely without a indexoutofbounds
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="sample"></param>
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

    public static void RenderScreenBuffer(bool colored = true)
    {
        //Resize buffer if we need to
        ConsoleInterface.CheckBufferSize();

        var pos = ConsoleInterface.GetCursorPos();

        //Initialize cursor position to origin and hide it
        var visible = Console.CursorVisible;
        Console.CursorVisible = false;
        ConsoleInterface.SetCursorPos(0, 0);

        //We do +1 or it crashes ┌( ಠ_ಠ)┘
        var rgbBuffer = ConsoleColorConverter.GetEmptyColorCharBuffer(xLength + 1);

        //Due to weirdness, we need to do ylength - 1
        //Iterate each cell of the screenbuffer
        for (int y = 0; y < yLength - 1; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                var current = screenBuffer[y, x];

                if (colored)
                {
                    //If its colored we set the portion of the buffer to our console character information
                    ConsoleColorConverter.SetRGBBuffer(
                        current.foreColor, current.bgColor,
                        current.chr, ref rgbBuffer,
                        x * ConsoleColorConverter.GetColorBufferLength());
                }
                else
                {
                    //Otherwise just set the basic char
                    rgbBuffer[x] = current.chr;
                }
            }

            var currentHash = QuickHash.GetHash(rgbBuffer);

            if (currentHash != previousHashes[y])
            {
                //Write it
                Console.Write(rgbBuffer);
            }

            ConsoleInterface.SetCursorPos(0, y + 1);

            previousHashes[y] = currentHash;
        }

        //Reset cursor position and visibility
        ConsoleInterface.SetCursorPos(pos);
        Console.CursorVisible = visible;
    }
}