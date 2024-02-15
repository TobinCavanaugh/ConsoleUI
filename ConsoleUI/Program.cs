using System.Diagnostics;
using ConsoleUI;

public class Program
{
    public static void Main(string[] args)
    {
        WindowsInterface.Init();
        ConsoleInterface.Init();
        ConsoleBuffer.RebuildBuffer();

        UIElement element = new()
        {
            upperLeft = new Vec2(20, 20),
            bottomRight = new Vec2(80, 80)
        };
        UIElement other = new()
        {
            upperLeft = (0, 0),
            bottomRight = (10, 10),
        };


        Stopwatch frameTime = new();

        while (true)
        {
            frameTime.Start();
            ConsoleInterface.CheckBufferSize();

            ConsoleKeyInfo raw = default;
            if (Console.KeyAvailable)
            {
                raw = Console.ReadKey(true);
            }

            var key = raw.Key;
            var mod = raw.Modifiers;

            ConsoleInterface.MoveCursor(0, 0);

            if (key == ConsoleKey.UpArrow)
            {
                element.upperLeft.y--;
                element.bottomRight.y--;
                ConsoleInterface.MoveCursor(0, -1);
                continue;
            }

            if (key == ConsoleKey.DownArrow)
            {
                element.upperLeft.y++;
                element.bottomRight.y++;
                ConsoleInterface.MoveCursor(0, 1);
                continue;
            }

            if (key == ConsoleKey.LeftArrow)
            {
                element.upperLeft.x--;
                element.bottomRight.x--;
                ConsoleInterface.MoveCursor(-1, 0);
                continue;
            }

            if (key == ConsoleKey.RightArrow)
            {
                element.upperLeft.x++;
                element.bottomRight.x++;
                ConsoleInterface.MoveCursor(1, 0);
                continue;
            }

            if (key == ConsoleKey.Enter)
            {
                Console.Write("\n");
                continue;
            }

            if (mod == ConsoleModifiers.Control && key == ConsoleKey.S)
            {
                var stream = Console.OpenStandardOutput();

                var stw = new StreamReader(stream);

                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\file.txt", stw.ReadToEnd());
            }

            ConsoleBuffer.FillRegion((0, 0), ConsoleInterface.GetConsoleDimensions(), new ConsoleCharacter()
            {
                chr = ' ',
                bgColor = ConsoleInterface.clearColor.ToRGB(),
                foreColor = ConsoleInterface.clearColor.ToRGB(),
            });

            element.Render();
            other.Render();
            element.Render();

            ConsoleBuffer.WriteAt("Cool text", (0, 0), ConsoleColor.Green);

            ConsoleBuffer.WriteAt(WindowsInterface.GetWindowRect_().ToString(), (0, 0));
            ConsoleBuffer.WriteAt(ConsoleInterface.GetMousePos().ToString(), (0, 1));
            ConsoleBuffer.RenderScreenBuffer();

            ConsoleBuffer.WriteAt(frameTime.ElapsedMilliseconds + "ms", (0, 2));

            frameTime.Restart();
            ConsoleBuffer.RenderScreenBuffer();
            

            // Thread.Sleep(100);
        }
    }
}