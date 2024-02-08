using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleUI;

public static class WindowsInterface
{
    //Thanks to Nishan on https://stackoverflow.com/a/64359971/21769995
    // Begin
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(ref Point lpPoint);
    //End

    // Import necessary WinAPI functions
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    public struct Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public override string ToString()
        {
            return $"L:{Left} T:{Top} R:{Right} B:{Bottom}";
        }
    }

    private static IntPtr procPtr;


    public static void Init()
    {
       // procPtr = GetConsoleWindow();
       // var wind = Process.GetCurrentProcess().MainWindowHandle;

       procPtr = GetConsoleWindow();

       if (procPtr == IntPtr.Zero)
       {
           Environment.Exit(0);
           throw new Exception();
       }
    }

    static Rect r = new Rect();

    public static Rect GetWindowRect_()
    {
        GetWindowRect(procPtr, out r);
        return r;
    }
}