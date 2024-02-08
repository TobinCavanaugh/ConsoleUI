using System.ComponentModel.Design;
using System.Diagnostics;
using ConsoleUI;

public class Program
{
    public static void Main(string[] args)
    {
        ConsoleInterface.Init();
        WindowsInterface.Init();
        ConsoleBuffer.RebuildBuffer();


        int it = 0;
        var dim = ConsoleInterface.GetConsoleDimensions();
        
        string combined = "";
        while (true)
        {
            Random r = new Random();
            Stopwatch stopwatch = new();
            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                stopwatch.Stop();
                int off = 10;
                var s = it.ToString()[^1].ToString();
                var vec = (r.Next(dim.x - off) + off, r.Next(dim.y));
                stopwatch.Start();
                ConsoleInterface.WriteColoredAt(s, ConsoleColor.Blue, vec);
            }
            stopwatch.Stop();
            combined += $"|{stopwatch.ElapsedMilliseconds}ms";
            // ConsoleInterface.WriteColoredAt(stopwatch.ElapsedMilliseconds + "ms", ConsoleColor.White, (0, it));
            it++;
            
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/speed.txt", combined);
        }


        UIElement element = new()
        {
            upperLeft = new Vec2(20, 20),
            bottomRight = new Vec2(80, 80)
        };
        UIElement other = new()
        {
            upperLeft = (0, 0),
            bottomRight = (10, 10),
            color = ConsoleColor.Red
        };


        Stopwatch frameTime = new();

        while (true)
        {
            ConsoleInterface.CheckBufferSize();
            frameTime.Start();

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

                var sw = new StreamReader(stream);

                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\file.txt", sw.ReadToEnd());
            }

            // ConsoleInterface.Clear();

            element.Render();
            other.Render();

            ConsoleBuffer.WriteAt(frameTime.ElapsedMilliseconds + "ms",
                (0, ConsoleInterface.GetConsoleDimensions().y - 2), ConsoleColor.Cyan);
            // ConsoleInterface.WriteColoredAt(stopwatch.ElapsedMilliseconds + "ms", ConsoleColor.Blue, (0, 100));
            // ConsoleInterface.WriteColoredAt(ConsoleInterface.GetMousePos().ToString(), ConsoleColor.Cyan, (20, 100));
            // ConsoleInterface.WriteColoredAt(WindowsInterface.GetWindowRect_().ToString(), ConsoleColor.Cyan, (50, 100));


            ConsoleBuffer.FillRegion((0, 0), ConsoleInterface.GetConsoleDimensions(),
                new ConsoleCharacter() {chr = '0', color = ConsoleColor.Red, dirty = true});


            ConsoleBuffer.RenderScreenBuffer();
            // ConsoleBuffer.Clear();

            // Console.Write(raw.KeyChar);
            frameTime.Stop();
            // Thread.Sleep((int) Math.Max(16 - stopwatch.ElapsedMilliseconds, 0));

            frameTime.Reset();
        }
    }
}