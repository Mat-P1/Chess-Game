using ChessConsoleApp.Chessboard;

namespace ChessConsoleApp;

public class DisplayScreen
{
    public static void DisplayGameBoard(GameBoard gameBoardScreen)
    {
        for (int i = 0; i < gameBoardScreen.GameBoardRows; i++)
        {
            for (int j = 0; j < gameBoardScreen.GameBoardColumns; j++)
            {
                if (gameBoardScreen.ReturnPiecesPositions(i, j) == null)
                {
                    Console.Write("- ");
                }
                else
                {
                    Console.Write(gameBoardScreen.ReturnPiecesPositions(i, j) + " ");
                }
            }
            Console.WriteLine();
        }
    }
}