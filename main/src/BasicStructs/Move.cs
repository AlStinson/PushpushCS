using static Pushpush.Direction;
using static Pushpush.MoveKind;
using static Pushpush.Team;

namespace Pushpush
{
  public struct Move
  {
    public readonly Team team;
    public readonly Position pos;
    public readonly Direction dir;
    public readonly MoveKind kind;
    public Position nextPos { get { return pos + dir; } }
    public Direction nextDir { get { return dir + (kind.isNormal ? zero : nextPos.normalDirection); } }
    public Move next { get { return new Move(team, nextPos, nextDir, Normal); } }
    public bool isValid { get { return isValidNormal || isValidDeflected; } }
    public bool isValidNormal { get { return isValidCommon && kind.isNormal; } }
    public bool isValidDeflected { get { return isValidCommon && kind.isDeflected && pos.isLateral && nextPos.isLateral; } }
    public bool isValidCommon { get { return team.isTeam && pos.isValid && nextPos.isValid && dir.isValid; } }

    public Move(Team team, Position pos, Direction dir, MoveKind kind)
    {
      this.team = team;
      this.pos = pos;
      this.dir = dir;
      this.kind = kind;
    }

    public bool isSimetricOf(Move m) { return team == m.team.next && dir.isSimetricOf(m.dir) && pos.isSimetricOf(m.pos); }

    public override string ToString() { return pos.ToString() + dir.ToString() + kind.ToString(); }

  }
}