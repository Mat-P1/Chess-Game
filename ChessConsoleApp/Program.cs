using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Exceptions;
using ChessConsoleApp.ChessRules;

namespace ChessConsoleApp;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ChessMatch newMatch = new ChessMatch();
            
            while (!newMatch.MatchFinished)
            {
                Console.Clear();
                DisplayScreen.DisplayGameBoard(newMatch.ChessMatchGameBoard);
                
                Console.Write("\nOrigin: ");
                Position origin = DisplayScreen.ReadChessPosition().ToArrayPosition();
                
                Console.Write("Destination: ");
                Position destination = DisplayScreen.ReadChessPosition().ToArrayPosition();
                
                newMatch.PieceMovement(origin, destination);
            }
        }
        catch (GameBoardExceptions error)
        {
            Console.WriteLine(error.Message);
        }
    }
}