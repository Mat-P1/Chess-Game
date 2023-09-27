using ChessConsoleApp.Chessboard;

namespace ChessConsoleApp.ChessRules;

public class ChessPosition
{
    private char ColumnChessPosition { get; }
    private int RowChessPosition { get; }
    public ChessPosition(char columnChessPosition, int rowChessPosition)
    {
        ColumnChessPosition = columnChessPosition;
        RowChessPosition = rowChessPosition;
    }
    
    public Position ToArrayPosition()
    {
        return new Position(8 - RowChessPosition, ColumnChessPosition - 'a');
    }

    public override string ToString()
    {
        return "" + ColumnChessPosition + RowChessPosition;
    }
}