using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;
using ChessConsoleApp.Chessboard.Exceptions;
using ChessConsoleApp.ChessRules;

namespace ChessConsoleApp;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            GameBoard newGameBoard = new GameBoard(8, 8);
            
            newGameBoard.PlacePiece(new Rook(Color.Black, newGameBoard), new Position(0, 0));
            newGameBoard.PlacePiece(new Rook(Color.Black, newGameBoard), new Position(1, 3));
            newGameBoard.PlacePiece(new King(Color.Black, newGameBoard), new Position(0, 4));
            
            DisplayScreen.DisplayGameBoard(newGameBoard);
        }
        catch (GameBoardExceptions error)
        {
            Console.WriteLine(error.Message);
        }
    }
}