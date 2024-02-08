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

    private Vec2 oldUpperLeft;
    private Vec2 oldBottomRight;

    public void Render()
    {
        var dim = ConsoleInterface.GetConsoleDimensions();
        int xMin = (int) ((upperLeft.x / 100f) * dim.x);
        int xMax = (int) ((bottomRight.x / 100f) * dim.x);
        int yMin = (int) ((upperLeft.y / 100f) * dim.y);
        int yMax = (int) ((bottomRight.y / 100f) * dim.y);

        if (!upperLeft.Equals(oldUpperLeft) || !bottomRight.Equals(oldBottomRight))
        {
            ConsoleBuffer.FillRegion(
                (
                    ((int) (oldUpperLeft.x / 100f) * dim.x, (int) (oldUpperLeft.y / 100f) * dim.y)),
                ((int) (oldBottomRight.x / 100f) * dim.x, (int) (oldBottomRight.y / 100f) * dim.y), new ConsoleCharacter()
                {
                    chr = '0',
                    color = ConsoleColor.Red,
                    dirty = true
                });
        }


        var cursorPos = ConsoleInterface.GetCursorPos();

        string bar = "".PadLeft((xMax - xMin), drawingCharacter);
        for (int y = yMin; y < yMax; y++)
        {
            ConsoleBuffer.WriteAt(bar, (xMin, y), this.color);
        }

        ConsoleInterface.SetCursorPos(cursorPos);

        oldUpperLeft = upperLeft;
        oldBottomRight = bottomRight;
    }
}