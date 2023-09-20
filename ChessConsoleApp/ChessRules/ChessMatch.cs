using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;

namespace ChessConsoleApp.ChessRules;

public class ChessMatch
{
    public GameBoard ChessMatchGameBoard { get; private set; }
    private int _matchTurn;
    private Color _currentPlayer;
    public bool MatchFinished { get; private set; }

    public ChessMatch()
    {
        ChessMatchGameBoard = new GameBoard(8, 8);
        _matchTurn = 1;
        _currentPlayer = Color.White;
        PlacePiecesOnBoard();
        MatchFinished = false;
    }

    private void PlacePiecesOnBoard()
    {
        ChessMatchGameBoard.PlacePiece(new Rook(Color.White, ChessMatchGameBoard), new ChessPosition('c', 1).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new Rook(Color.White, ChessMatchGameBoard), new ChessPosition('c', 2).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new Rook(Color.White, ChessMatchGameBoard), new ChessPosition('d', 2).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new Rook(Color.White, ChessMatchGameBoard), new ChessPosition('e', 2).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new Rook(Color.White, ChessMatchGameBoard), new ChessPosition('e', 1).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new King(Color.White, ChessMatchGameBoard), new ChessPosition('d', 1).ToArrayPosition());
        
        ChessMatchGameBoard.PlacePiece(new Rook(Color.Black, ChessMatchGameBoard), new ChessPosition('c', 7).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new Rook(Color.Black, ChessMatchGameBoard), new ChessPosition('c', 8).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new Rook(Color.Black, ChessMatchGameBoard), new ChessPosition('d', 7).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new Rook(Color.Black, ChessMatchGameBoard), new ChessPosition('e', 7).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new Rook(Color.Black, ChessMatchGameBoard), new ChessPosition('e', 8).ToArrayPosition());
        ChessMatchGameBoard.PlacePiece(new King(Color.Black, ChessMatchGameBoard), new ChessPosition('d', 8).ToArrayPosition());
    }

    public void PieceMovement(Position origin, Position destination)
    {
        Piece pieceMove = ChessMatchGameBoard.RemovePiece(origin);
        pieceMove.IncrementsNumberOfMoves();
        Piece capturedPiece = ChessMatchGameBoard.RemovePiece(destination);
        ChessMatchGameBoard.PlacePiece(pieceMove, destination);
    }
}