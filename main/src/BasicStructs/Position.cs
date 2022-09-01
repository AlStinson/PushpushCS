using Pushpush;

using static Pushpush.Team;
using static Pushpush.Direction;

namespace Pushpush
{
  public struct Position
  {
    public readonly int x;
    public readonly int y;

    private static int xMin = 1;
    private static int xMax = 7;
    private static int yMin = 1;
    private static int yMax = 7;

    public bool isValid { get { return x.between(xMin, xMax) && y.between(yMin, yMax); } }
    public bool isLateral { get { return x == xMin || x == xMax || y == yMin || y == yMax; } }
    public Team teamGoal
    {
      get
      {
        if (y == yMin - 1 && x.between(xMin, xMax)) return White;
        if (y == yMax + 1 && x.between(xMin, xMax)) return Black;
        return None;
      }
    }
    public Direction normalDirection
    {
      get
      {
        Direction result = zero;
        if (x == xMin) result += right;
        if (x == xMax) result += left;
        if (y == yMin) result += up;
        if (y == yMax) result += down;
        return result;
      }
    }

    public Position(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public static Position fromHashCode(int i)
    {
      if (i < 0 || i > 48) throw new System.Exception();
      return new Position(i % 7 + 1, i / 7 + 1);
    }

    public static Position operator +(Direction d, Position p) { return new Position(d.x + p.x, d.y + p.y); }
    public static Position operator +(Position p, Direction d) { return new Position(d.x + p.x, d.y + p.y); }
    public static Direction operator -(Position p, Position q) { return new Direction(p.x - q.x, p.y - q.y); }

    public override string ToString() { return x.ToString() + y.ToString(); }
    public override int GetHashCode() { return (x - 1) + 7 * (y - 1); }

    public bool isSimetricOf(Position p) { return x + p.x == xMax + 1 && y + p.y == yMax + 1; }

  }
}
