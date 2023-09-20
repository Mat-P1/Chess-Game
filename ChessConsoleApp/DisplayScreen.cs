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
                if (gameBoardScreen.ReturnPiecePosition(i, j) == null)
                {
                    Console.Write("- ");
                }   
                else
                {
                    DisplayPieceColor(gameBoardScreen.ReturnPiecePosition(i, j));
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }

    public static ChessPosition ReadChessPosition()
    {
        string readPosition = Console.ReadLine() ?? string.Empty;
        char readColumn = readPosition[0];
        int readRow  = int.Parse(readPosition[1] + "");
        return new ChessPosition(readColumn, readRow);
    }
    
    public static void DisplayPieceColor(Piece pieceAppearance)
    {
        if (pieceAppearance.PieceColor == Color.White)
        {
            Console.Write(pieceAppearance);
        }
        else
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(pieceAppearance);
            Console.ForegroundColor = aux;
        }
    }
}