using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules.Pieces;

public class Pawn : Piece
{
    private ChessMatch _match;
    public Pawn(Color pieceColor, GameBoard pieceBoard, ChessMatch match) : base(pieceColor, pieceBoard)
    {
        _match = match;
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
            
            // En Passant
            if (PiecePosition.RowPosition == 3) 
            {
                Position left = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 1);
                if (PieceBoard.IsValidPosition(left) && IsThereAdversary(left) &&
                    PieceBoard.ReturnPiecePosition(left) == _match.VulnerableEnPassant)
                {
                    moveArray[left.RowPosition - 1, left.ColumnPosition] = true;
                } // Left
                
                Position right = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 1);
                if (PieceBoard.IsValidPosition(right) && IsThereAdversary(right) &&
                    PieceBoard.ReturnPiecePosition(right) == _match.VulnerableEnPassant)
                {
                    moveArray[right.RowPosition - 1, right.ColumnPosition] = true;
                } // Right
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
            
            // En Passant
            if (PiecePosition.RowPosition == 4) 
            {
                Position left = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 1);
                if (PieceBoard.IsValidPosition(left) && IsThereAdversary(left) &&
                    PieceBoard.ReturnPiecePosition(left) == _match.VulnerableEnPassant)
                {
                    moveArray[left.RowPosition + 1, left.ColumnPosition] = true;
                } // Left
                
                Position right = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 1);
                if (PieceBoard.IsValidPosition(right) && IsThereAdversary(right) &&
                    PieceBoard.ReturnPiecePosition(right) == _match.VulnerableEnPassant)
                {
                    moveArray[right.RowPosition + 1, right.ColumnPosition] = true;
                } // Right
            } 
        }
        
        return moveArray;
    }
     
     public override string ToString()
     {
         return "P";
     }
}