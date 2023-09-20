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

    public Piece ReturnPiecesPositions(int pieceRow, int pieceColumn)
    {
        return _gameBoardPieces[pieceRow, pieceColumn];
    }
}