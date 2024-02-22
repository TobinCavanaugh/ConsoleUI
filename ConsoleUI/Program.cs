using System.Diagnostics;

namespace ConsoleUI;

public class Program
{
    private static float[][] nodes =
    {
        new float[] {-1, -1, -1}, new float[] {-1, -1, 1}, new float[] {-1, 1, -1}, new float[] {-1, 1, 1},
        new float[] {1, -1, -1}, new float[] {1, -1, 1}, new float[] {1, 1, -1}, new float[] {1, 1, 1}
    };


    static int[][] edges =
    {
        new[] {0, 1}, new[] {1, 3}, new[] {3, 2}, new[] {2, 0}, new[] {4, 5},
        new[] {5, 7}, new[] {7, 6}, new[] {6, 4}, new[] {0, 4}, new[] {1, 5},
        new[] {2, 6}, new[] {3, 7}
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

        // UIElement element = new()
        // {
        //     upperLeft = new Vec2(20, 20),
        //     bottomRight = new Vec2(80, 80)
        // };
        // UIElement other = new()
        // {
        //     upperLeft = (0, 0),
        //     bottomRight = (10, 10),
        // };


        Stopwatch frameTime = new();

        int ms = 0;

        int s = 4;
        Scale(s, s, s);
        RotateCuboid(MathF.PI / 4, MathF.Atan(MathF.Sqrt(2)));

        float deltaTime = 0;


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

            float rot = MathF.PI / 10f;

            if (key == ConsoleKey.UpArrow)
            {
                ConsoleInterface.MoveCursor(0, -1);
                RotateCuboid(0, rot);
                continue;
            }

            if (key == ConsoleKey.DownArrow)
            {
                ConsoleInterface.MoveCursor(0, 1);
                RotateCuboid(0, -rot);
                continue;
            }

            if (key == ConsoleKey.LeftArrow)
            {
                ConsoleInterface.MoveCursor(-1, 0);
                RotateCuboid(rot, 0);
                continue;
            }

            if (key == ConsoleKey.RightArrow)
            {
                ConsoleInterface.MoveCursor(1, 0);
                RotateCuboid(-rot, 0);
                continue;
            }

            if (key == ConsoleKey.W)
            {
                Scale(1.5f, 1.5f, 1.5f);
            }

            if (key == ConsoleKey.S)
            {
                Scale(.75f, .75f, .75f);
            }

            if (mod == ConsoleModifiers.Control && key == ConsoleKey.S)
            {
                var stream = Console.OpenStandardOutput();

                var stw = new StreamReader(stream);

                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\file.txt", stw.ReadToEnd());
            }

            if (mod == ConsoleModifiers.Control && key == ConsoleKey.W)
            {
                Environment.Exit(0);
            }

            RotateCuboid(deltaTime, 0);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ConsoleBuffer.Clear();

            var dim = ConsoleInterface.GetConsoleDimensions();

            var hx = dim.x / 2;
            var hy = dim.y / 2;

            //TODO, X needs to be scaled up cuz chars are 9:16

            foreach (var edge in edges)
            {
                float[] xy1 = nodes[edge[0]];
                float[] xy2 = nodes[edge[1]];

                ConsoleInterface.DrawLine(
                    ((int) MathF.Round(xy1[0]) + hx, (int) MathF.Round(xy1[1]) + hy),
                    ((int) MathF.Round(xy2[0]) + hx, (int) MathF.Round(xy2[1]) + hy),
                    new ConsoleCharacter('#', ConsoleColor.Blue, ConsoleColor.Blue)
                );
            }

            foreach (var node in nodes)
            {
                ConsoleBuffer.SafeSet(
                    (int) MathF.Round(node[0]) + hx,
                    (int) Math.Round(node[1]) + hy,
                    new ConsoleCharacter()
                        {chr = '#', foreColor = ConsoleColor.Cyan.ToRGB(), bgColor = ConsoleColor.Black});
            }

            ConsoleBuffer.WriteAt($"Framerate: {(1000f / ms).ToString("F2")}", (0, 0), ConsoleColor.White);

            ConsoleBuffer.RenderScreenBuffer();

            ms = (int) frameTime.ElapsedMilliseconds;

            deltaTime = ms / 1000f;

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