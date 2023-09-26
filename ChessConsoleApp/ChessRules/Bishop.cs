using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules;

public class Bishop : Piece
{
    public Bishop(Color pieceColor, GameBoard pieceBoard) : base(pieceColor, pieceBoard)
    {
    }
    
    private bool CanMove(Position position)
    {
        Piece piece = PieceBoard.ReturnPiecePosition(position);
        return piece == null || piece.PieceColor != PieceColor;
    }
    
    public override bool[,] PossibleMoves()
    {
        bool[,] moveArray = new bool[PieceBoard.GameBoardRows, PieceBoard.GameBoardColumns];
        Position movePosition = new Position(0, 0);
        
        // Upper Diagonal Right
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition - 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null && PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor)
            {
                break;
            }
            movePosition.SetValues(movePosition.RowPosition - 1, movePosition.ColumnPosition - 1);
        }
        
        // Upper Diagonal Left
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition + 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null && PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor)
            {
                break;
            }
            movePosition.SetValues(movePosition.RowPosition -1, movePosition.ColumnPosition + 1);
        }
        
        // Lower Diagonal Left
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition + 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null && PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor)
            {
                break;
            }
            movePosition.SetValues(movePosition.RowPosition + 1, movePosition.ColumnPosition + 1);
        }
        
        // Lower Diagonal Right
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition - 1);
        while (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            if (PieceBoard.ReturnPiecePosition(movePosition) != null && PieceBoard.ReturnPiecePosition(movePosition).PieceColor != PieceColor)
            {
                break;
            }
            movePosition.SetValues(movePosition.RowPosition + 1, movePosition.ColumnPosition - 1);
        }

        return moveArray;
    }
    
    public override string ToString()
    {
        return "B";
    }
}