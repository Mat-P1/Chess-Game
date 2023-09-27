using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.Chessboard;

public abstract class Piece
{
    public Position PiecePosition { get; set; }
    public Color PieceColor { get; }
    public int NumberOfMoves { get; private set; }
    protected GameBoard PieceBoard { get; }

    protected Piece(Color pieceColor, GameBoard pieceBoard)
    {
        PiecePosition = null!;
        PieceColor = pieceColor;
        PieceBoard = pieceBoard;
        NumberOfMoves = 0;
    }
    
    public abstract bool[,] PossibleMoves();

    public bool CanPieceMoveTo(Position position)
    {
        return PossibleMoves()[position.RowPosition, position.ColumnPosition];
    }

    public bool IsThereAnyPossibleMove()
    {
        var possibleMoves = PossibleMoves();

        for (var i = 0; i < PieceBoard.GameBoardRows; i++)
        for (var j = 0; j < PieceBoard.GameBoardColumns; j++)
            if (possibleMoves[i, j])
                return true;
        return false;
    }

    public void IncrementsNumberOfMoves()
    {
        NumberOfMoves++;
    }

    public void DecreaseNumberOfMoves()
    {
        NumberOfMoves--;
    }
}