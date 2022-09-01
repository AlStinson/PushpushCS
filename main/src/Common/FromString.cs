using System;
using static Pushpush.MoveKind;

namespace Pushpush
{
  public static class FromString
  {
    private static MoveKind ToMoveKind(this string s)
    {
      if (s == "n") return Normal;
      if (s == "d") return Deflected;
      throw new FromStringNotMatchException(s, "MoveKind");
    }

    private static int To1dDirection(this string s)
    {
      if (s == "p") return 1;
      if (s == "z") return 0;
      if (s == "n") return -1;
      throw new FromStringNotMatchException(s, "Direction");
    }

    private static Direction ToDirection(this string s)
    {
      string x = s.Substring(0, 1);
      string y = s.Substring(1, 1);
      return new Direction(x.To1dDirection(), y.To1dDirection());
    }

    private static Position ToPosition(this string s)
    {
      string x = s.Substring(0, 1);
      string y = s.Substring(1, 1);
      return new Position(int.Parse(x), int.Parse(y));
    }

    public static Move ToMove(this string s, Team t)
    {
      if (s.Length != 5) throw new FromStringLengthException(s, "Move", 5);
      string pos = s.Substring(0, 2);
      string dir = s.Substring(2, 2);
      string kind = s.Substring(4, 1);
      return new Move(t, pos.ToPosition(), dir.ToDirection(), kind.ToMoveKind());
    }
  }

  public class FromStringLengthException : Exception
  {
    public FromStringLengthException(string s, string type, int expectedLength) :
      base(s + " cannot be a " + type + " as it has length " + type.Length.ToString() + " but it was expected " + expectedLength.ToString())
    { }
  }

  public class FromStringNotMatchException : Exception
  {
    public FromStringNotMatchException(string s, string type) :
      base(s + " cannot be a " + type + " becose it doesnot match")
    { }
  }

}