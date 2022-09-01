using System;
using static Pushpush.Team;

namespace Pushpush
{
  public struct Team
  {
    private readonly int hash;
    public bool isNone { get { return hash == 0; } }
    public bool isTeam { get { return hash.between(1, 2); } }
    public bool isWhite { get { return hash == 1; } }
    public bool isBlack { get { return hash == 2; } }
    public Team next
    {
      get
      {
        if (isWhite) return Black;
        if (isBlack) return White;
        return None;
      }
    }

    private Team(int hash)
    {
      this.hash = hash;
    }

    public static Team None = new Team(0);
    public static Team White = new Team(1);
    public static Team Black = new Team(2);

    public static Team fromHashCode(int i)
    {
      if (i<0 || i>2) throw new Exception();
      return new Team(i);
    }

    public static bool operator ==(Team t1, Team t2) { return t1.hash == t2.hash; }
    public static bool operator !=(Team t1, Team t2) { return t1.hash != t2.hash; }

    public override bool Equals(Object? o) { return o is Team && o.GetHashCode() == GetHashCode(); }
    public override int GetHashCode() { return hash; }
    public override string ToString()
    {
      switch (hash)
      {
        case 1: return "w";
        case 2: return "b";
        default: return "n";
      }
    }
  }
}