namespace Pushpush
{
  public struct Direction
  {
    public readonly int x;
    public readonly int y;
    public bool isValid { get { return (x * x + y * y).between(1, 2); } }

    public Direction(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public static Direction zero = new Direction(0, 0);
    public static Direction right = new Direction(1, 0);
    public static Direction left = new Direction(-1, 0);
    public static Direction up = new Direction(0, 1);
    public static Direction down = new Direction(0, -1);
    public static List<Direction> allDirections = new List<Direction> { right, left, up, down, right + up, right + down, left + up, left + down };

    public static string ToString1d(int z) {return z>0 ? "p" : z==0 ? "z" : "n";}

    public static Direction operator +(Direction d1, Direction d2) { return new Direction(d1.x + d2.x, d1.y + d2.y); }
    public static Direction operator -(Direction d1, Direction d2) { return new Direction(d1.x - d2.x, d1.y - d2.y); }

    public override string ToString() { return ToString1d(x)+ToString1d(y); }
    public override int GetHashCode() { return (x+1)+3*(y+1);}

    public bool isSimetricOf(Direction d) { return x + d.x == 0 && y + d.y == 0; }

  }

}