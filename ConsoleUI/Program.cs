using System.Diagnostics;
using ConsoleUI;

public class Program
{
    private static float[][] nodes =
    {
        new float[] {-1, -1, -1}, new float[] {-1, -1, 1}, new float[] {-1, 1, -1}, new float[] {-1, 1, 1},
        new float[] {1, -1, -1}, new float[] {1, -1, 1}, new float[] {1, 1, -1}, new float[] {1, 1, 1}
    };


    int[][] edges =
    {
        new int[] {0, 1}, new int[] {1, 3}, new int[] {3, 2}, new int[] {2, 0}, new int[] {4, 5},
        new int[] {5, 7}, new int[] {7, 6}, new int[] {6, 4}, new int[] {0, 4}, new int[] {1, 5},
        new int[] {2, 6}, new int[] {3, 7}
    };

    public static void Main(string[] args)
    {
        //back: 48
        //fore: 38

        //TODO Optimize ANSI codes by doing fancy line checks to see if all colors are the same, will be a huge speed & time save
        //TODO Top line overlapping/rendering twice incorrectly


        WindowsInterface.Init();
        ConsoleInterface.Init();

        // var buffer = ConsoleColorConverter.GetEmptyColorCharBuffer(2);
        //
        // ConsoleColorConverter.SetRGBBuffer((0, 0, 255), (255, 0, 0), 'A', ref buffer, 0);
        // ConsoleColorConverter.SetRGBBuffer((255, 0, 255), (0, 255, 0), 'B', ref buffer, buffer.Length / 2);
        //
        // //printf("\033[38;2;0;0;255m\033[48;2;255;0;0mA\033[0m");
        //
        // Console.WriteLine(buffer);
        // // Console.WriteLine(Regex.Escape(new string(nbuffer)));
        //
        // while (true)
        // {
        // }

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

        int ms = 0;

        int s = 4;
        Scale(s, s, s);
        RotateCuboid(MathF.PI / 4, MathF.Atan(MathF.Sqrt(2)));

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

            float rot = MathF.PI / 5f;

            if (key == ConsoleKey.UpArrow)
            {
                element.upperLeft.y--;
                element.bottomRight.y--;
                ConsoleInterface.MoveCursor(0, -1);
                RotateCuboid(0,rot);
                continue;
            }

            if (key == ConsoleKey.DownArrow)
            {
                element.upperLeft.y++;
                element.bottomRight.y++;
                ConsoleInterface.MoveCursor(0, 1);
                RotateCuboid(0, -rot);
                continue;
            }

            if (key == ConsoleKey.LeftArrow)
            {
                element.upperLeft.x--;
                element.bottomRight.x--;
                ConsoleInterface.MoveCursor(-1, 0);
                RotateCuboid(rot, 0);
                continue;
            }

            if (key == ConsoleKey.RightArrow)
            {
                element.upperLeft.x++;
                element.bottomRight.x++;
                ConsoleInterface.MoveCursor(1, 0);
                RotateCuboid(-rot, 0);
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

            // element.Render();
            // other.Render();
            // element.Render();

            // ConsoleBuffer.WriteAt(ms + "ms", (2, 2), ConsoleColor.Cyan);

            // ConsoleBuffer.FillRegion((0, 0), (1, ConsoleInterface.GetConsoleDimensions().y + 1), new ConsoleCharacter()
            // {
            // chr = ' ',
            // bgColor = (255, 0, 0),
            // foreColor = (255, 0, 0)
            // });

            // ConsoleBuffer.WriteAt(":)", (0, 0), ConsoleColor.Cyan);
            // ConsoleBuffer.WriteAt(ms + "ms", (0, 0), ConsoleColor.Cyan);

            // ConsoleBuffer.SafeSet(0, 0,
            // new ConsoleCharacter() {bgColor = (255, 0, 0), chr = '@', foreColor = (0, 0, 255)});
            // ConsoleBuffer.SafeSet(0, 1,
            // new ConsoleCharacter() {bgColor = (255, 0, 0), chr = '?', foreColor = (0, 0, 255)});
            // ConsoleBuffer.SafeSet(0, ConsoleInterface.GetConsoleDimensions().y - 1,
            // new ConsoleCharacter() {bgColor = (255, 0, 0), chr = '#', foreColor = (0, 0, 255)});

            var dim = ConsoleInterface.GetConsoleDimensions();

            foreach (var node in nodes)
            {
                ConsoleBuffer.SafeSet(
                    (int) MathF.Round(node[0]) + dim.x / 2,
                    (int) Math.Round(node[1]) - 4 + dim.y / 2,
                    new ConsoleCharacter()
                        {chr = '#', foreColor = ConsoleColor.Cyan.ToRGB(), bgColor = ConsoleColor.Cyan.ToRGB()});
            }


            ConsoleBuffer.RenderScreenBuffer();

            ms = (int) frameTime.ElapsedMilliseconds;

            frameTime.Restart();
        }
    }

    static void Scale(float x, float y, float z)
    {
        foreach (var node in nodes)
        {
            node[0] *= x;
            node[1] *= y;
            node[2] *= z;
        }
    }

    static void RotateCuboid(float angleX, float angleY)
    {
        float sinX = MathF.Sin(angleX);
        float cosX = MathF.Cos(angleX);

        float sinY = MathF.Sin(angleY);
        float cosY = MathF.Cos(angleY);

        foreach (var node in nodes)
        {
            var x = node[0];
            var y = node[1];
            var z = node[2];

            node[0] = x * cosX - z * sinX;
            node[2] = z * cosX + x * sinX;

            z = node[2];

            node[1] = y * cosY - z * sinY;
            node[2] = z * cosY + y * sinY;
        }
    }
}