using static Pushpush.MoveKind;

namespace Pushpush
{
  public struct MoveKind
  {
    public readonly bool isNormal;
    public bool isDeflected { get { return !isNormal; } }

    private MoveKind(bool isNormal)
    {
      this.isNormal = isNormal;
    }

    public static MoveKind Normal = new MoveKind(true);
    public static MoveKind Deflected = new MoveKind(false);

    public override string ToString() { return isNormal ? "n" : "d"; }
    public override int GetHashCode() { return isNormal ? 0 : 1; } 

  }
}