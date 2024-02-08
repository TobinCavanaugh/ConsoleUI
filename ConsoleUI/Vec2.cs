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

    public override string ToString()
    {
        return $"({x}, {y})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Vec2 otherVec)
        {
            return (x == otherVec.x && y == otherVec.y);
        }
        
        return base.Equals(obj);
    }

    public bool Equals(Vec2 other)
    {
        return x == other.x && y == other.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }
}