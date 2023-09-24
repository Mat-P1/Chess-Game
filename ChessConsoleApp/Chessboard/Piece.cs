using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.Chessboard;

public abstract class Piece
{
    public Position PiecePosition { get; set; }
    public Color PieceColor { get; set; }
    public int NumberOfMoves { get; protected set; }
    public GameBoard PieceBoard { get; protected set; }

    public Piece(Color pieceColor, GameBoard pieceBoard)
    {
        PiecePosition = null;
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
        bool[,] possibleMoves = PossibleMoves();
        for (int i = 0; i < PieceBoard.GameBoardRows; i++)
        {
            for (int j = 0; j < PieceBoard.GameBoardColumns; j++)
            {
                if (possibleMoves[i, j])
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    public void IncrementsNumberOfMoves()
    {
        NumberOfMoves++;
    }
}