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

                try
                {
                    DisplayScreen.DisplayMatch(newMatch);
                
                    Console.Write("\nOrigin: ");
                    Position origin = DisplayScreen.ReadChessPosition().ToArrayPosition();
                    newMatch.ValidateOriginPosition(origin);

                    bool[,] possiblePositions = newMatch.ChessMatchGameBoard.ReturnPiecePosition(origin).PossibleMoves();
                
                    Console.Clear();
                    DisplayScreen.DisplayGameBoard(newMatch.ChessMatchGameBoard, possiblePositions);
                
                    Console.Write("\nDestination: ");
                    Position destination = DisplayScreen.ReadChessPosition().ToArrayPosition();
                    newMatch.ValidateTargetPosition(origin, destination);
                
                    newMatch.MakeAMove(origin, destination);
                }
                
                catch (GameBoardExceptions error)
                {
                    Console.WriteLine(error.Message);
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                }
            }
        }
        catch (GameBoardExceptions error)
        {
            Console.WriteLine(error.Message);
        }
    }
}