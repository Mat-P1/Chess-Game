namespace ChessConsoleApp.Chessboard;

public class Position
{
    public int RowPosition { get; set; }
    public int ColumnPosition { get; set; }

    public Position(int rowPosition, int columnPosition)
    {
        RowPosition = rowPosition;
        ColumnPosition = columnPosition;
    }

    public override string ToString()
    {
        return $"{RowPosition}, {ColumnPosition}";
    }
}   