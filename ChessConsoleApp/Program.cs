using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;
using ChessConsoleApp.Chessboard.Exceptions;
using ChessConsoleApp.ChessRules;

namespace ChessConsoleApp;

internal class Program
{
    public static void Main(string[] args)
    {
        ChessPosition newPosition = new ChessPosition('a', 1);
        Console.WriteLine(newPosition.ToArrayPosition());
    }
}