using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;
using ChessConsoleApp.Chessboard.Exceptions;

namespace ChessConsoleApp.ChessRules;

public class ChessMatch
{
    public GameBoard ChessMatchGameBoard { get; private set; }
    public int MatchTurn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool MatchFinished { get; private set; }

    public ChessMatch()
    {
        ChessMatchGameBoard = new GameBoard(8, 8);
        MatchTurn = 1;
        CurrentPlayer = Color.White;
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

    public void ValidateOriginPosition(Position position)
    {
        if (ChessMatchGameBoard.ReturnPiecePosition(position) == null)
        {
            throw new GameBoardExceptions("There is no piece on this position");
        }
        
        if (CurrentPlayer != ChessMatchGameBoard.ReturnPiecePosition(position).PieceColor)
        {
            throw new GameBoardExceptions("The chosen piece is not yours");
        }

        if (!ChessMatchGameBoard.ReturnPiecePosition(position).IsThereAnyPossibleMove())
        {
            throw new GameBoardExceptions("There is no possible moves for the chosen piece");
        }
    }
    
    public void ValidateTargetPosition(Position origin, Position destination)
    {
        if (!ChessMatchGameBoard.ReturnPiecePosition(origin).CanPieceMoveTo(destination))
        {
            throw new GameBoardExceptions("The chosen piece can't move to target position");
        }
    }

    private void ChangePlayer()
    {
        if (CurrentPlayer == Color.White)
        {
            CurrentPlayer = Color.Black;
        }
        else
        {
            CurrentPlayer = Color.White;
        }
    }

    public void MakeAMove(Position origin, Position destination)
    {
        PieceMovement(origin, destination);
        MatchTurn++;
        ChangePlayer();
    }
}