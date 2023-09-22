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

    public void IncrementsNumberOfMoves()
    {
        NumberOfMoves++;
    }
}