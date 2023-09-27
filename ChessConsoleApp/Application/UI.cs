using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;
using ChessConsoleApp.ChessRules;

namespace ChessConsoleApp.Application;

public static class Ui
{
    public static void DisplayMatch(ChessMatch newMatch)
    {
        Console.Clear();
        DisplayGameBoard(newMatch.ChessMatchGameBoard);
        DisplayCapturedPieces(newMatch);
        Console.WriteLine($"\nTurn: {newMatch.MatchTurn}");
        
        if (!newMatch.MatchFinished)
        {
            Console.WriteLine($"Current Player: {newMatch.CurrentPlayer}");
            if (newMatch.Check) Console.WriteLine("CHECK!");
        }
        else
        {
            Console.WriteLine($"CHECKMATE!\nWinner: {newMatch.CurrentPlayer}");
        }
    }

    public static void DisplayGameBoard(GameBoard gameBoardScreen, bool[,] possiblePositions)
    {
        const ConsoleColor highlightedBackground = ConsoleColor.DarkGray;
        var originalBackground = Console.BackgroundColor;
        
        for (var i = 0; i < gameBoardScreen.GameBoardRows; i++)
        {
            Console.Write(8 - i + " ");
            for (var j = 0; j < gameBoardScreen.GameBoardColumns; j++)
            {
                Console.BackgroundColor = possiblePositions[i, j] ? highlightedBackground : originalBackground;
                DisplayPiece(gameBoardScreen.ReturnPiecePosition(i, j));
                Console.BackgroundColor = originalBackground;
            }

            Console.WriteLine();
        }

        Console.WriteLine("  a b c d e f g h");
        Console.BackgroundColor = originalBackground;
    }

    public static ChessPosition ReadChessPosition()
    {
        var readPosition = Console.ReadLine() ?? string.Empty;
        var readColumn = readPosition[0];
        var readRow = int.Parse(readPosition[1] + "");
        return new ChessPosition(readColumn, readRow);
    }
    
    private static void DisplayGameBoard(GameBoard gameBoardScreen)
    {
        for (var i = 0; i < gameBoardScreen.GameBoardRows; i++)
        {
            Console.Write(8 - i + " ");
            for (var j = 0; j < gameBoardScreen.GameBoardColumns; j++)
                DisplayPiece(gameBoardScreen.ReturnPiecePosition(i, j));
            Console.WriteLine();
        }

        Console.WriteLine("  a b c d e f g h");
    }
    
    private static void DisplayPiece(Piece piece)
    {
        if (piece == null)
        {
            Console.Write("_ ");
        }
        else
        {
            if (piece.PieceColor == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                var aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }

            Console.Write(" ");
        }
    }

    private static void DisplayPiecesHashSet(HashSet<Piece> collection)
    {
        Console.Write("[");
        foreach (var piece in collection) Console.Write(piece + " ");
        Console.Write("]");
    }

    private static void DisplayCapturedPieces(ChessMatch newMatch)
    {
        Console.WriteLine("\nCaptured Pieces");
        Console.Write("Whites: ");
        DisplayPiecesHashSet(newMatch.CapturedPieces(Color.White));

        Console.Write("\nBlacks: ");
        var aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        DisplayPiecesHashSet(newMatch.CapturedPieces(Color.Black));
        Console.ForegroundColor = aux;

        Console.WriteLine();
    }
}