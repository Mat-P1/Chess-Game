using ChessConsoleApp.Chessboard.Exceptions;
using ChessConsoleApp.ChessRules;

namespace ChessConsoleApp.Application;

internal static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var newMatch = new ChessMatch();

            while (!newMatch.MatchFinished)
                try
                {
                    Console.Clear();
                    Ui.DisplayMatch(newMatch);

                    Console.Write("\nOrigin: ");
                    var origin = Ui.ReadChessPosition().ToArrayPosition();
                    newMatch.ValidateOriginPosition(origin);

                    var possiblePositions = newMatch.ChessMatchGameBoard.ReturnPiecePosition(origin).PossibleMoves();

                    Console.Clear();
                    Ui.DisplayGameBoard(newMatch.ChessMatchGameBoard, possiblePositions);

                    Console.Write("\nDestination: ");
                    var destination = Ui.ReadChessPosition().ToArrayPosition();
                    newMatch.ValidateTargetPosition(origin, destination);

                    newMatch.MakeAMove(origin, destination);
                }

                catch (GameBoardExceptions error)
                {
                    Console.WriteLine(error.Message);
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                }

            Console.Clear();
            Ui.DisplayMatch(newMatch);
        }

        catch (GameBoardExceptions error)
        {
            Console.WriteLine(error.Message);
        }
    }
}