using System.Collections.Immutable;

using static Pushpush.Team;
using static Pushpush.Piece;

namespace Pushpush
{
  sealed class Game : BaseGame
  {

    public static int repetitionRule = 3;
    public static int noEndRule = 500;

    public Winner winner {get; private set;}
    public ImmutableList<ImmutableDictionary<Position, Piece>> boardsRecord { get; private set; }
    public ImmutableList<Move> movesRecord { get; private set; }

    public int turn { get { return boardsRecord.Count + 1; } }

    public Game() : base(InitialBoard, White) 
    { 
      this.winner = Winner.fromTeam(None);
      this.boardsRecord = ImmutableList.Create<ImmutableDictionary<Position, Piece>>();
      this.movesRecord = ImmutableList.Create<Move>();
    }

    public static Dictionary<Position, Piece> InitialBoard
    {
      get
      {
        Dictionary<Position, Piece> board = new Dictionary<Position, Piece>();
        board.Add(new Position(1, 1), WhitePiece(1));
        board.Add(new Position(2, 1), WhitePiece(2));
        board.Add(new Position(3, 1), WhitePiece(3));
        board.Add(new Position(4, 1), WhitePiece(4));
        board.Add(new Position(5, 1), WhitePiece(3));
        board.Add(new Position(6, 1), WhitePiece(2));
        board.Add(new Position(7, 1), WhitePiece(1));
        board.Add(new Position(1, 7), BlackPiece(1));
        board.Add(new Position(2, 7), BlackPiece(2));
        board.Add(new Position(3, 7), BlackPiece(3));
        board.Add(new Position(4, 7), BlackPiece(4));
        board.Add(new Position(5, 7), BlackPiece(3));
        board.Add(new Position(6, 7), BlackPiece(2));
        board.Add(new Position(7, 7), BlackPiece(1));
        board.Add(new Position(4, 4), Ball);
        return board;
      }
    }

    public override bool TryMakeMove(Move m)
    {
      if (!validMoves.Contains(m)) return false;

      boardsRecord = boardsRecord.Add(board);
      movesRecord = movesRecord.Add(m);

      MakeBoardMove(m);

      winner = Winner.fromTeam(board.First(p => p.Value.isBall).Key.teamGoal.next);
      if (ocurrences() >= repetitionRule - 1) setWinner(Winner.Draw);
      GenerateValidMoves();
      if (validMoves.Count == 0) setWinner(currentPlayer.next);
      return true;
    }

    protected override void GenerateValidMoves()
    {
      if (!winner.isNone) validMoves = ImmutableList.Create<Move>();
      else base.GenerateValidMoves();
    }

    protected override bool isMoveValid(Move m)
    {
      if (turn == 2 && movesRecord[0].isSimetricOf(m)) return false;
      return base.isMoveValid(m);
    }

    private int ocurrences()
    {
      int result = 0;
      int length = board.Count;
      foreach(ImmutableDictionary<Position, Piece> board2 in boardsRecord)
      {
        if (board2.Count != length) continue;

        foreach (KeyValuePair<Position, Piece> pair in board)
        {
          if (!board2.ContainsKey(pair.Key)) goto NotSameBoard;
          if (board2[pair.Key] != pair.Value) goto NotSameBoard;
        }
        result++;
        
        NotSameBoard:;
      }

      return result;
    }

    private void setWinner(Winner w)
    {
      if (!winner.isNone) return;

      winner = w;
      validMoves = ImmutableList.Create<Move>();
    }

    public void setWinner(Team t)
    {
      setWinner(Winner.fromTeam(t));
    }

    public Winner getWinner() { return winner; }

    public override string ToString()
    {
      string result = "";
      result += winner.ToString() + "|";
      result += base.ToString();
      return result;
    }

  }
}