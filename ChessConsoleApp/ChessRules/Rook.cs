using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules;

public class Rook : Piece
{
    public Rook(Color pieceColor, GameBoard pieceBoard) : base(pieceColor, pieceBoard)
    {
    }

    public override string ToString()
    {
        return "R";
    }
}