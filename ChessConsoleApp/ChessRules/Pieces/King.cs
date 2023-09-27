using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules.Pieces;

public class King : Piece
{
    private readonly ChessMatch _match;

    public King(Color pieceColor, GameBoard pieceBoard, ChessMatch match) : base(pieceColor, pieceBoard)
    {
        _match = match;
    }
    
    public override bool[,] PossibleMoves()
    {
        var moveArray = new bool[PieceBoard.GameBoardRows, PieceBoard.GameBoardColumns];
        var movePosition = new Position(0, 0);

        // Above
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;

        // Upper Diagonal Right
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition + 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;

        // Right
        movePosition.SetValues(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;

        // Lower Diagonal Right
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition + 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;

        // Below
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;

        // Lower Diagonal Left
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition - 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;

        // Left
        movePosition.SetValues(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;

        // Upper Diagonal Left
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition - 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;

        // Castling
        if (NumberOfMoves == 0 && !_match.Check)
        {
            // Short 
            var shortRookPosition = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 3);

            if (TestRookCastling(shortRookPosition))
            {
                var positionOneToRight = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 1);
                var positionTwoToRight = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 2);

                if (PieceBoard.ReturnPiecePosition(positionOneToRight) == null &&
                    PieceBoard.ReturnPiecePosition(positionTwoToRight) == null)
                    moveArray[PiecePosition.RowPosition, PiecePosition.ColumnPosition + 2] = true;
            }

            // Long
            var longRookPosition = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 4);

            if (TestRookCastling(longRookPosition))
            {
                var positionOneToLeft = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 1);
                var positionTwoToLeft = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 2);
                var positionThreeToLeft = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 3);
                
                if (PieceBoard.ReturnPiecePosition(positionOneToLeft) == null && PieceBoard.ReturnPiecePosition(
                                                                                  positionTwoToLeft) == null
                                                                              && PieceBoard.ReturnPiecePosition(
                                                                                  positionThreeToLeft) == null)
                    moveArray[PiecePosition.RowPosition, PiecePosition.ColumnPosition - 2] = true;
            }
        }

        return moveArray;
    }

    public override string ToString()
    {
        return "K";
    }

    private bool CanMove(Position position)
    {
        var piece = PieceBoard.ReturnPiecePosition(position);
        return piece == null || piece.PieceColor != PieceColor;
    }

    private bool TestRookCastling(Position position)
    {
        var p = PieceBoard.ReturnPiecePosition(position);
        return p != null && p is Rook && p.PieceColor == PieceColor && p.NumberOfMoves == 0;
    }
}