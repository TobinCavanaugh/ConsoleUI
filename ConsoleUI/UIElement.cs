namespace ConsoleUI;

public class UIElement
{
    /// <summary>
    /// Relative makes the rendering relative to the console size, meaning <see cref="upperLeft"/> and the <see cref="bottomRight"/> (in ranges from 0-100) 
    /// </summary>
    public bool relative = true;

    public Vec2 upperLeft = new Vec2(0, 0);
    public Vec2 bottomRight = new Vec2(100, 100);


    // public char drawingCharacter = '\u2588';
    public char drawingCharacter = '0';

    public void Render()
    {
        var dim = ConsoleInterface.GetConsoleDimensions();
        int xMin = (int) ((upperLeft.x / 100f) * dim.x);
        int xMax = (int) ((bottomRight.x / 100f) * dim.x);
        int yMin = (int) ((upperLeft.y / 100f) * dim.y);
        int yMax = (int) ((bottomRight.y / 100f) * dim.y);

        ConsoleBuffer.FillRegion((xMin, yMin), (xMax, yMax), new ConsoleCharacter()
        {
            chr = drawingCharacter,
            bgColor = ConsoleColor.Gray.ToRGB(),
            foreColor = ConsoleColor.Gray.ToRGB(),
        });

        ConsoleBuffer.WriteAt("Fancy Text", ((xMin + xMax) / 2, (yMin + yMax) / 2), ConsoleColor.Red.ToRGB(),
            (-1, -1, -1));
        // ConsoleBuffer.WriteAt("\u2500", (xMin, xMax), ConsoleColor.Red, ConsoleColor.Black);
    }
}