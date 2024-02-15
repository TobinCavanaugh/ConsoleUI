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

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool AttachConsole(uint dwProcessId);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern bool FreeConsole();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);


    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
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

    public static RECT GetWindowRect_()
    {
        var rr = new RECT();
        GetWindowRect(GetConsoleWindow(), ref rr);
        return rr;
    }

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

        InitConsoleColors();
    }

    private static void InitConsoleColors()
    {
        // Get the handle to the standard output stream
        var handle = GetStdHandle(STD_OUTPUT_HANDLE);

        // Get the current console mode
        uint mode;
        if (!GetConsoleMode(handle, out mode))
        {
            Console.Error.WriteLine("Failed to get console mode");
            return;
        }

        // Enable the virtual terminal processing mode
        mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
        if (!SetConsoleMode(handle, mode))
        {
            Console.Error.WriteLine("Failed to set console mode");
            return;
        }
    }


    // P/Invoke declarations
    private const int STD_OUTPUT_HANDLE = -11;
    private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
}