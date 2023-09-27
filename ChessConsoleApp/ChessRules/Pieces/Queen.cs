using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules.Pieces;

public class Queen : Piece
{
    public Queen(Color pieceColor, GameBoard pieceBoard) : base(pieceColor, pieceBoard)
    {
    }
    
    public override bool[,] PossibleMoves()
    {
        var moveArray = new bool[PieceBoard.GameBoardRows, PieceBoard.GameBoardColumns];
        var movePosition = new Position(0, 0);

        // Above
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null &&
                PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor) break;
            movePosition.RowPosition -= 1;
        }

        // Right
        movePosition.SetValues(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null &&
                PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor) break;
            movePosition.ColumnPosition += 1;
        }

        // Below
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null &&
                PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor) break;
            movePosition.RowPosition += 1;
        }

        // Left
        movePosition.SetValues(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null &&
                PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor) break;
            movePosition.ColumnPosition -= 1;
        }

        // Upper Diagonal Right
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition - 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null &&
                PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor) break;
            movePosition.SetValues(movePosition.RowPosition - 1, movePosition.ColumnPosition - 1);
        }

        // Upper Diagonal Left
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition + 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null &&
                PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor) break;
            movePosition.SetValues(movePosition.RowPosition - 1, movePosition.ColumnPosition + 1);
        }

        // Lower Diagonal Left
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition + 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null &&
                PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor) break;
            movePosition.SetValues(movePosition.RowPosition + 1, movePosition.ColumnPosition + 1);
        }

        // Lower Diagonal Right
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition - 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null &&
                PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor) break;
            movePosition.SetValues(movePosition.RowPosition + 1, movePosition.ColumnPosition - 1);
        }

        return moveArray;
    }

    public override string ToString()
    {
        return "Q";
    }
    
    private bool CanMove(Position position)
    {
        var piece = PieceBoard.ReturnPiecePosition(position);
        return piece == null || piece.PieceColor != PieceColor;
    }
}