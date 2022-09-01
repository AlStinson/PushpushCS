using System.Collections.Immutable;

using static Pushpush.Direction;
using static Pushpush.MoveKind;

namespace Pushpush
{
  public class BaseGame
  {
    public ImmutableDictionary<Position, Piece> board {get; protected set;}
    public ImmutableList<Move> validMoves {get; protected set;}
    public Team currentPlayer { get; protected set; }

    internal BaseGame(Dictionary<Position, Piece> board, Team currentPlayer)
    {
      this.board = board.ToImmutableDictionary<Position, Piece>();
      this.validMoves = ImmutableList.Create<Move>();
      this.currentPlayer = currentPlayer;
    }

    public virtual void Start()
    {
      GenerateValidMoves();
    }

    protected virtual void GenerateValidMoves()
    {
      ImmutableList<Move>.Builder validMovesBuilder = ImmutableList.CreateBuilder<Move>();
      foreach (KeyValuePair<Position, Piece> pair in board)
      {
        if (!pair.Value.belongsToTeam(currentPlayer)) continue;

        foreach (Direction dir in allDirections)
        {
          Move normalMove = new Move(currentPlayer, pair.Key, dir, Normal);
          Move deflectedMove = new Move(currentPlayer, pair.Key, dir, Deflected);
          if (isMoveValid(normalMove)) validMovesBuilder.Add(normalMove);
          if (isMoveValid(deflectedMove)) validMovesBuilder.Add(deflectedMove);
        }
      }
      validMoves = validMovesBuilder.ToImmutable();
    }

    protected virtual bool isMoveValid(Move m)
    {
      if (!m.isValid) return false;
      if (!board.ContainsKey(m.pos)) return false;
      Piece p1 = board[m.pos];
      if (p1.team != m.team) return false;
      if (!board.ContainsKey(m.nextPos)) return m.kind.isNormal; // Posible fallo, añadir m.nextPos.isValid;
      Piece p2 = board[m.nextPos];
      if (m.kind.isDeflected && p2.isNotBall) return false;
      if (p1.kills(p2)) return m.kind.isNormal;
      if (!(p1 > p2)) return false;
      return isMoveQuasiValid(m.next);
    }

    private bool isMoveQuasiValid(Move m)
    {
      Piece p1 = board[m.pos];
      if (p1.isBall && !m.nextPos.isValid && m.nextPos.teamGoal != m.team.next) return false;
      if (!board.ContainsKey(m.nextPos)) return true;
      Piece p2 = board[m.nextPos];
      if (!(p1 > p2)) return false;
      return isMoveQuasiValid(m.next);
    }

    public virtual bool TryMakeMove(Move m)
    {
      if (!validMoves.Contains(m)) return false;
      MakeBoardMove(m);
      currentPlayer = currentPlayer.next;
      return true;
    }

    protected void MakeBoardMove(Move m)
    {
      ImmutableDictionary<Position, Piece>.Builder boardBuilder = board.ToBuilder();
      MakeBoardMove(m,boardBuilder);
      board = boardBuilder.ToImmutable();
    }

    private void MakeBoardMove(Move m, ImmutableDictionary<Position, Piece>.Builder boardBuilder)
    {
      Piece p1 = boardBuilder[m.pos];
      boardBuilder.Remove(m.pos);
      if (boardBuilder.ContainsKey(m.nextPos))
      {
        Piece p2 = boardBuilder[m.nextPos];
        if (!p1.kills(p2)) MakeBoardMove(m.next, boardBuilder);
        else { boardBuilder.Remove(m.nextPos); }
      }
      if (m.nextPos.isValid || p1.isBall) boardBuilder.Add(m.nextPos, p1);
    }

    public override string ToString()
    {
      string result = "";
      foreach (KeyValuePair<Position, Piece> p in board) result += p.Key.ToString() + p.Value.ToString();
      result += currentPlayer.ToString() + "|";
      foreach (Move m in validMoves) result += m.ToString();
      return result;
    }
  }
}