using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules;

public class King : Piece
{
    public King(Color pieceColor, GameBoard pieceBoard) : base(pieceColor, pieceBoard)
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
        return moveArray;
    }
    
    public override string ToString()
    {
        return "K";
    }
}