namespace ConsoleUI;

public struct Vec2
{
    public int x;
    public int y;

    public Vec2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vec2()
    {
        
    }

    public static implicit operator Vec2((int x, int y) pos)
    {
        return new Vec2(pos.x, pos.y);
    }
}