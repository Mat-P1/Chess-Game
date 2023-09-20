using ChessConsoleApp.Chessboard.Exceptions;

namespace ChessConsoleApp.Chessboard;

public class GameBoard
{
    public int GameBoardRows { get; set; }
    public int GameBoardColumns { get; set; }
    private readonly Piece[,] _gameBoardPieces;

    public GameBoard(int gameBoardRows, int gameBoardColumns)
    {
        GameBoardRows = gameBoardRows;
        GameBoardColumns = gameBoardColumns;
        _gameBoardPieces = new Piece[gameBoardRows, gameBoardColumns];
    }
    
    public Piece ReturnPiecePosition(int pieceRow, int pieceColumn)
    {
        return _gameBoardPieces[pieceRow, pieceColumn];
    }
    
    public Piece ReturnPiecePosition(Position position)
    {
        return _gameBoardPieces[position.RowPosition, position.ColumnPosition];
    }

    public bool IsValidPosition(Position validPosition)
    {
        if (validPosition.RowPosition < 0 || validPosition.RowPosition >= GameBoardRows ||
            validPosition.ColumnPosition < 0 || validPosition.ColumnPosition >= GameBoardRows)
        {
            return false;
        }
        return true;
    }

    public void ValidatePosition(Position validatePosition)
    {
        if (!IsValidPosition(validatePosition))
        {
            throw new GameBoardExceptions("Invalid position!");
        }
    }

    public bool DoesPieceExists(Position pieceExists)
    {
        ValidatePosition(pieceExists);
        return ReturnPiecePosition(pieceExists) != null;
    }
    
    public void PlacePiece(Piece setPiece, Position setPosition)
    {
        if (DoesPieceExists(setPosition))
        {
            throw new GameBoardExceptions("A piece is already placed here!");
        }
        _gameBoardPieces[setPosition.RowPosition, setPosition.ColumnPosition] = setPiece;
        setPiece.PiecePosition = setPosition;
    }

    public Piece RemovePiece(Position piecePosition)
    {
        if (ReturnPiecePosition(piecePosition) == null)
        {
            return null;
        }
        Piece aux = ReturnPiecePosition(piecePosition);
        aux.PiecePosition = null;
        _gameBoardPieces[piecePosition.RowPosition, piecePosition.ColumnPosition] = null;
        return aux;
    }
}