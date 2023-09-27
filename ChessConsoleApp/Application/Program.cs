using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Exceptions;
using ChessConsoleApp.ChessRules;

namespace ChessConsoleApp.Application;

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
                    Console.Clear();
                    UI.DisplayMatch(newMatch);
                
                    Console.Write("\nOrigin: ");
                    Position origin = UI.ReadChessPosition().ToArrayPosition();
                    newMatch.ValidateOriginPosition(origin);

                    bool[,] possiblePositions = newMatch.ChessMatchGameBoard.ReturnPiecePosition(origin).PossibleMoves();
                
                    Console.Clear();
                    UI.DisplayGameBoard(newMatch.ChessMatchGameBoard, possiblePositions);
                
                    Console.Write("\nDestination: ");
                    Position destination = UI.ReadChessPosition().ToArrayPosition();
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
            Console.Clear();
            UI.DisplayMatch(newMatch);
        }
        
        catch (GameBoardExceptions error)
        {
            Console.WriteLine(error.Message);
        }
    }
}