using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules;

public class Pawn : Piece
{
    public Pawn(Color pieceColor, GameBoard pieceBoard) : base(pieceColor, pieceBoard)
    {
    }

    public override string ToString()
    {
        return "P";
    }

    private bool IsThereAdversary(Position position)
    {
        Piece piece = PieceBoard.ReturnPiecePosition(position);
        return piece != null && piece.PieceColor != PieceColor;
    }

    private bool FreePosition(Position position)
    {
        return PieceBoard.ReturnPiecePosition(position) == null;
    }
    
     public override bool[,] PossibleMoves()
    {
        bool[,] moveArray = new bool[PieceBoard.GameBoardRows, PieceBoard.GameBoardColumns];
        Position movePosition = new Position(0, 0);
        
        // White Pawn moves
        if (PieceColor == Color.White)
        {
            movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition);
            if (PieceBoard.IsValidPosition(movePosition) && FreePosition(movePosition))
            {
                moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            }
            
            movePosition.SetValues(PiecePosition.RowPosition - 2, PiecePosition.ColumnPosition);
            if (PieceBoard.IsValidPosition(movePosition) && FreePosition(movePosition) && NumberOfMoves == 0)
            {
                moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            }
            
            movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition - 1);
            if (PieceBoard.IsValidPosition(movePosition) && IsThereAdversary(movePosition))
            {
                moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            }
            
            movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition + 1);
            if (PieceBoard.IsValidPosition(movePosition) && IsThereAdversary(movePosition))
            {
                moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            }
        }
        // Black Pawn moves
        else
        {
            movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition);
            if (PieceBoard.IsValidPosition(movePosition) && FreePosition(movePosition))
            {
                moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            }
            
            movePosition.SetValues(PiecePosition.RowPosition + 2, PiecePosition.ColumnPosition);
            if (PieceBoard.IsValidPosition(movePosition) && FreePosition(movePosition) && NumberOfMoves == 0)
            {
                moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            }
            
            movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition - 1);
            if (PieceBoard.IsValidPosition(movePosition) && IsThereAdversary(movePosition))
            {
                moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            }
            
            movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition + 1);
            if (PieceBoard.IsValidPosition(movePosition) && IsThereAdversary(movePosition))
            {
                moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
            }
        }
        
        return moveArray;
    }
}