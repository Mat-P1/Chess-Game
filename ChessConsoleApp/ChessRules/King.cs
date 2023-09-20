using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules;

public class King : Piece
{
    public King(Color pieceColor, GameBoard pieceBoard) : base(pieceColor, pieceBoard)
    {
    }

    public override string ToString()
    {
        return "K";
    }
}