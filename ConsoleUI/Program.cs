// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Text;
using ConsoleUI;

public class Program
{
    public static void Main(string[] args)
    {
        ConsoleInterface.Init();
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


        Stopwatch stopwatch = new();
        while (true)
        {
            var raw = Console.ReadKey(true);
            stopwatch.Start();
            var key = raw.Key;
            var mod = raw.Modifiers;

            ConsoleInterface.MoveCursor(0, 0);


            if (key == ConsoleKey.UpArrow)
            {
                ConsoleInterface.MoveCursor(0, -1);
                continue;
            }

            if (key == ConsoleKey.DownArrow)
            {
                ConsoleInterface.MoveCursor(0, 1);
                continue;
            }

            if (key == ConsoleKey.LeftArrow)
            {
                ConsoleInterface.MoveCursor(-1, 0);
                continue;
            }

            if (key == ConsoleKey.RightArrow)
            {
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

            ConsoleInterface.Clear();
            element.Render();
            other.Render();

            Console.Write(raw.KeyChar);
            stopwatch.Stop();
            ConsoleInterface.WriteColoredAt(stopwatch.ElapsedMilliseconds + "ms", ConsoleColor.Blue, new Vec2(0, 100));
            stopwatch.Reset();
        }
    }
}