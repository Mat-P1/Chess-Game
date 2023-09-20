using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

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
                if (gameBoardScreen.ReturnPiecesPositions(i, j) == null)
                {
                    Console.Write("- ");
                }   
                else
                {
                    DisplayPieceColor(gameBoardScreen.ReturnPiecesPositions(i, j));
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
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