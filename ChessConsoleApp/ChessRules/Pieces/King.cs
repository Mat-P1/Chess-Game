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

    private bool CanMove(Position position)
    {
        Piece piece = PieceBoard.ReturnPiecePosition(position);
        return piece == null || piece.PieceColor != PieceColor;
    }

    private bool TestRookCastling(Position position)
    {
        Piece p = PieceBoard.ReturnPiecePosition(position);
        return p != null && p is Rook && p.PieceColor == PieceColor && p.NumberOfMoves == 0;
    }
    
    public override bool[,] PossibleMoves()
    {
        bool[,] moveArray = new bool[PieceBoard.GameBoardRows, PieceBoard.GameBoardColumns];
        Position movePosition = new Position(0, 0);
        
        // Above
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        // Upper Diagonal Right
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition + 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        // Right
        movePosition.SetValues(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        // Lower Diagonal Right
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition + 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        // Below
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        // Lower Diagonal Left
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition - 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        // Left
        movePosition.SetValues(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        // Upper Diagonal Left
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition - 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        // Castling
        if (NumberOfMoves == 0 && !_match.Check)
        {
            // Short 
            Position shortRookPosition = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 3);
            
            if (TestRookCastling(shortRookPosition))
            {
                Position positionOneToRight = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 1);
                Position positionTwoToRight = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition + 2);

                if (PieceBoard.ReturnPiecePosition(positionOneToRight) == null && PieceBoard.ReturnPiecePosition(positionTwoToRight) == null)
                {
                    moveArray[PiecePosition.RowPosition, PiecePosition.ColumnPosition + 2] = true;
                }
            }
            
            // Long
            Position longRookPosition = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 4);
            
            if (TestRookCastling(longRookPosition))
            {
                Position positionOneToLeft = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 1);
                Position positionTwoToLeft = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 2);
                Position positionThreeToLeft = new Position(PiecePosition.RowPosition, PiecePosition.ColumnPosition - 3);
                if (PieceBoard.ReturnPiecePosition(positionOneToLeft) == null && PieceBoard.ReturnPiecePosition(positionTwoToLeft) == null 
                                                                              && PieceBoard.ReturnPiecePosition(positionThreeToLeft) == null)
                {
                    moveArray[PiecePosition.RowPosition, PiecePosition.ColumnPosition - 2] = true;
                }
            }
            
        }
        
        return moveArray;
    }
    public override string ToString()
    {
        return "K";
    }
}