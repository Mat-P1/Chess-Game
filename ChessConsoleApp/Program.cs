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

                bool[,] possiblePositions = newMatch.ChessMatchGameBoard.ReturnPiecePosition(origin).PossibleMoves();
                
                Console.Clear();
                DisplayScreen.DisplayGameBoard(newMatch.ChessMatchGameBoard, possiblePositions);
                
                Console.Write("\nDestination: ");
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