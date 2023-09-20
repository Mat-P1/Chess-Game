using ChessConsoleApp.Chessboard;

namespace ChessConsoleApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GameBoard newGameBoard = new GameBoard(8, 8);
        DisplayScreen.DisplayGameBoard(newGameBoard);
    }
}