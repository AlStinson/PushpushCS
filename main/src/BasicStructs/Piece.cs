using static Pushpush.Team;

namespace Pushpush
{

  public struct Piece
  {
    public readonly int force;
    public readonly Team team;
    public bool isBall { get { return team.isNone && force == 0; } }
    public bool isNotBall { get { return force.between(1, 4) && team.isTeam; } }
    public bool isValid { get { return isBall || isNotBall; } }

    private Piece(int force, Team team)
    {
      this.force = force;
      this.team = team;
    }

    public static Piece WhitePiece(int force) { return new Piece(force, White); }
    public static Piece BlackPiece(int force) { return new Piece(force, Black); }
    public static Piece Ball = new Piece(0, None);

    public static Piece fromHashCode(int i)
    {
      if (i == 0) return Ball;
      if (i < 1 || i > 8) throw new System.Exception();
      int teamHash = i % 2 == 0 ? 2 : 1;
      return new Piece((i - teamHash) / 2 + 1, Team.fromHashCode(teamHash));
    }

    public static bool operator <(Piece p1, Piece p2) { return p2 > p1; }
    public static bool operator >(Piece p1, Piece p2) { return p1.force > p2.force; }

    public static bool operator ==(Piece p1, Piece p2) { return p1.force == p2.force && p1.team == p2.team;}
    public static bool operator !=(Piece p1, Piece p2) { return p1.force != p2.force || p1.team != p2.team;}

    public override bool Equals(Object? o) { return o is Piece && o.GetHashCode() == GetHashCode(); }
    public override string ToString() { return team.ToString() + force.ToString(); }
    public override int GetHashCode() { return isBall ? 1 : 2 * force + team.GetHashCode() - 2; }

    public bool kills(Piece p) { return force == 1 && p.force == 4 && team != p.team; }
    public bool belongsToTeam(Team t) { return team == t; }

  }
}