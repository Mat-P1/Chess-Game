using ChessConsoleApp.Chessboard;

namespace ChessConsoleApp;

internal class Program
{
    public static void Main(string[] args)
    {
        Position p = new Position(3, 4);
        Console.WriteLine("Position: " + p);
    }
}