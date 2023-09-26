using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;
using ChessConsoleApp.ChessRules;

namespace ChessConsoleApp;

public class DisplayScreen
{
    public static void DisplayGameBoard(GameBoard gameBoardScreen)
    {
        for (int i = 0; i < gameBoardScreen.GameBoardRows; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < gameBoardScreen.GameBoardColumns; j++)
            {
                DisplayPiece(gameBoardScreen.ReturnPiecePosition(i, j));
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }
    
    public static void DisplayGameBoard(GameBoard gameBoardScreen, bool[,] possiblePositions)
    {
        ConsoleColor originalBackground = Console.BackgroundColor;
        ConsoleColor highlightedBackground = ConsoleColor.DarkGray;
        
        for (int i = 0; i < gameBoardScreen.GameBoardRows; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < gameBoardScreen.GameBoardColumns; j++)
            {
                
                if (possiblePositions[i, j])
                {
                    Console.BackgroundColor = highlightedBackground;
                }
                else
                {
                    Console.BackgroundColor = originalBackground;
                }
                
                DisplayPiece(gameBoardScreen.ReturnPiecePosition(i, j));
                Console.BackgroundColor = originalBackground;
            }
            Console.WriteLine();
        }
        Console.WriteLine(" a b c d e f g h");
        Console.BackgroundColor = originalBackground;
    }

    public static ChessPosition ReadChessPosition()
    {
        string readPosition = Console.ReadLine() ?? string.Empty;
        char readColumn = readPosition[0];
        int readRow  = int.Parse(readPosition[1] + "");
        return new ChessPosition(readColumn, readRow);
    }
    
    public static void DisplayPiece(Piece piece)
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
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }

    public static void DisplayPiecesHashSet(HashSet<Piece> collection)
    {
        Console.Write("[");
        
        foreach (Piece piece in collection)
        {
            Console.Write(piece + " ");
        }
        Console.Write("]");
    }

    public static void DisplayCapturedPieces(ChessMatch newMatch)
    {
        Console.WriteLine("\nCaptured Pieces");
        Console.Write("Whites: ");
        DisplayPiecesHashSet(newMatch.CapturedPieces(Color.White));
        Console.Write("\nBlacks: ");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        DisplayPiecesHashSet(newMatch.CapturedPieces(Color.Black));
        Console.ForegroundColor = aux;
        Console.WriteLine();
    }
    
    public static void DisplayMatch(ChessMatch newMatch)
    {
        Console.Clear();
        DisplayGameBoard(newMatch.ChessMatchGameBoard);
        DisplayCapturedPieces(newMatch);
        Console.WriteLine($"\nTurn: {newMatch.MatchTurn}");
        Console.WriteLine($"Current Player: {newMatch.CurrentPlayer}");
        if (newMatch.Check)
        {
            Console.WriteLine("CHECK!");
        }
    }
}