//[ESC]
//[
//3
//8
//;
//2
//;
//R1
//R2
//R3
//;
//G1
//G2
//G3
//;
//B1
//B2
//B3
//m
//Char

namespace ConsoleUI;

public static class ConsoleColorConverter
{
    public static Dictionary<ConsoleColor, (int r, int g, int b)> associations = new()
    {
        {ConsoleColor.Black, (0x00, 0x00, 0x00)},
        {ConsoleColor.DarkBlue, (0x00, 0x00, 0x80)},
        {ConsoleColor.DarkGreen, (0x00, 0x80, 0x00)},
        {ConsoleColor.DarkCyan, (0x00, 0x80, 0x80)},
        {ConsoleColor.DarkRed, (0x80, 0x00, 0x00)},
        {ConsoleColor.DarkMagenta, (0x80, 0x00, 0x80)},
        {ConsoleColor.DarkYellow, (0x80, 0x80, 0x00)},
        {ConsoleColor.DarkGray, (0x80, 0x80, 0x80)},
        {ConsoleColor.Blue, (0x00, 0x00, 0xFF)},
        {ConsoleColor.Green, (0x00, 0xFF, 0x00)},
        {ConsoleColor.Cyan, (0x00, 0xFF, 0xFF)},
        {ConsoleColor.Red, (0xFF, 0x00, 0x00)},
        {ConsoleColor.Magenta, (0xFF, 0x00, 0xFF)},
        {ConsoleColor.Yellow, (0xFF, 0xFF, 0x00)},
        {ConsoleColor.Gray, (0xC0, 0xC0, 0xC0)},
        {ConsoleColor.White, (0xFF, 0xFF, 0xFF)}
    };

    public static (int r, int g, int b) ToRGB(this ConsoleColor color)
    {
        return associations[color];
    }

    public static string Colorize(this char ogChar, int r, int g, int b)
    {
        //\x1b[0m
        return $"\x1b[38;2;{r:000};{g:000};{b:000}m{ogChar}";
    }

    public static char[] GetColorCharBuffer()
    {
        var len = Colorize(' ', 000, 000, 000);
        return len.ToCharArray();
    }

    public static void SetRGBBuffer((int r, int g, int b) color, char chr, ref char[] rgbBuffer, int layer)
    {
        string r = (color.r).ToString("000");
        string g = (color.g).ToString("000");
        string b = (color.b).ToString("000");
        string l = (layer).ToString("00");

        rgbBuffer[2] = l[0];
        rgbBuffer[3] = l[1];

        rgbBuffer[7] = r[0];
        rgbBuffer[8] = r[1];
        rgbBuffer[9] = r[2];

        rgbBuffer[11] = g[0];
        rgbBuffer[12] = g[1];
        rgbBuffer[13] = g[2];

        rgbBuffer[15] = b[0];
        rgbBuffer[16] = b[1];
        rgbBuffer[17] = b[2];

        rgbBuffer[19] = chr;
    }
}