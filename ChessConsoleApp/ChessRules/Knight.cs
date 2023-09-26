using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules;

public class Knight : Piece
{
    public Knight(Color pieceColor, GameBoard pieceBoard) : base(pieceColor, pieceBoard)
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
        
        // Possible L style moves for Knight
        
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition - 2);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        movePosition.SetValues(PiecePosition.RowPosition - 2, PiecePosition.ColumnPosition - 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        movePosition.SetValues(PiecePosition.RowPosition - 2, PiecePosition.ColumnPosition + 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        movePosition.SetValues(PiecePosition.RowPosition - 1, PiecePosition.ColumnPosition + 2);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition + 2);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        movePosition.SetValues(PiecePosition.RowPosition + 2, PiecePosition.ColumnPosition + 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        movePosition.SetValues(PiecePosition.RowPosition + 2, PiecePosition.ColumnPosition - 1);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        movePosition.SetValues(PiecePosition.RowPosition + 1, PiecePosition.ColumnPosition - 2);
        if (PieceBoard.IsValidPosition(movePosition) && CanMove(movePosition))
        {
            moveArray[movePosition.RowPosition, movePosition.ColumnPosition] = true;
        }
        
        return moveArray;
    }
        
    public override string ToString()
    {
        return "T";
    }
}