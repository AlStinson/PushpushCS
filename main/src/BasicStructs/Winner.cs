using System;
using static Pushpush.Team;

namespace Pushpush
{
  public struct Winner
  {
    private readonly int hash;
    public bool isNone { get { return hash == 0; } }
    public bool isTeam { get { return hash.between(1, 2); } }
    public bool isWhite { get { return hash == 1; } }
    public bool isBlack { get { return hash == 2; } }
    public bool isDraw { get { return hash == 3; } }

    private Winner(int hash)
    {
      this.hash = hash;
    }

    public static Winner None = new Winner(0);
    public static Winner White = new Winner(1);
    public static Winner Black = new Winner(2);
    public static Winner Draw = new Winner(3);

    public static Winner fromHashCode(int i)
    {
      if (i<0 || i>3) throw new Exception();
      return new Winner(i);
    }

    public static Winner fromTeam(Team t)
    {
        return new Winner(t.GetHashCode());
    }

    public static bool operator ==(Winner t1, Winner t2) { return t1.hash == t2.hash; }
    public static bool operator !=(Winner t1, Winner t2) { return t1.hash != t2.hash; }

    public override bool Equals(Object? o) { return o is Winner && o.GetHashCode() == GetHashCode(); }
    public override int GetHashCode() { return hash; }
    public override string ToString()
    {
      switch (hash)
      {
        case 1: return "w";
        case 2: return "b";
        case 3: return "d";
        default: return "n";
      }
    }
  }
}