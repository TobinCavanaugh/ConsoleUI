namespace ConsoleUI;

public class UIElement
{
    /// <summary>
    /// Relative makes the rendering relative to the console size, meaning <see cref="upperLeft"/> and the <see cref="bottomRight"/> (in ranges from 0-100) 
    /// </summary>
    public bool relative = true;

    public Vec2 upperLeft = new Vec2(0, 0);
    public Vec2 bottomRight = new Vec2(100, 100);

    public ConsoleColor color = ConsoleColor.White;
    public char drawingCharacter = '\u2588';

    public void Render()
    {
        if (relative)
        {
        }

        var dim = ConsoleInterface.GetConsoleDimensions();
        int xMin = (int) ((upperLeft.x / 100f) * dim.x);
        int xMax = (int) ((bottomRight.x / 100f) * dim.x);
        int yMin = (int) ((upperLeft.y / 100f) * dim.y);
        int yMax = (int) ((bottomRight.y / 100f) * dim.y);

        Vec2 ogPos = ConsoleInterface.GetCursorPos();
        ConsoleColor color = Console.ForegroundColor;

        string bar = "".PadLeft((xMax - xMin), drawingCharacter);
        for (int y = yMin; y < yMax; y++)
        {
            ConsoleInterface.SetCursorPos(xMin, y);
            ConsoleInterface.WriteColored(bar, this.color);
            // ConsoleInterface.WriteColored(drawingCharacter + "", this.color);
        }

        ConsoleInterface.SetCursorPos(ogPos);
    }
}