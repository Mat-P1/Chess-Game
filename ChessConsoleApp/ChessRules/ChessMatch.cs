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
    private HashSet<Piece> _piecesOnTheBoard = new();
    private HashSet<Piece> _capturedPieces = new();

    public ChessMatch()
    {
        ChessMatchGameBoard = new GameBoard(8, 8);
        MatchTurn = 1;
        CurrentPlayer = Color.White;
        PlacePiecesOnBoard();
        MatchFinished = false;
    }

    public void PlaceNewPiece(char column, int row, Piece piece)
    {
        ChessMatchGameBoard.PlacePiece(piece, new ChessPosition(column, row).ToArrayPosition());
        _piecesOnTheBoard.Add(piece);
    }
    
    private void PlacePiecesOnBoard()
    {
        PlaceNewPiece('c', 1, new Rook(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('c', 2, new Rook(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('d', 2, new Rook(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('e', 2, new Rook(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('e', 1, new Rook(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('d', 1, new King(Color.White, ChessMatchGameBoard));
        
        PlaceNewPiece('c', 7, new Rook(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('c', 8, new Rook(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('d', 7, new Rook(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('e', 7, new Rook(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('e', 8, new Rook(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('d', 8, new King(Color.Black, ChessMatchGameBoard));
    }

    public void PieceMovement(Position origin, Position destination)
    {
        Piece pieceMove = ChessMatchGameBoard.RemovePiece(origin);
        pieceMove.IncrementsNumberOfMoves();
        Piece capturedPiece = ChessMatchGameBoard.RemovePiece(destination);
        ChessMatchGameBoard.PlacePiece(pieceMove, destination);
        if (capturedPiece != null)
        {
            _capturedPieces.Add(capturedPiece);
        }
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

    public HashSet<Piece> CapturedPieces(Color color)
    {
        HashSet<Piece> aux = new HashSet<Piece>();
        foreach (Piece x in _capturedPieces)
        {
            if (x.PieceColor == color)
            {
                aux.Add(x);
            }
        }
        return aux;
    }
    
    public HashSet<Piece> PiecesInGame(Color color)
    {
        HashSet<Piece> aux = new HashSet<Piece>();
        foreach (Piece x in _piecesOnTheBoard)
        {
            if (x.PieceColor == color)
            {
                aux.Add(x);
            }
        }
        aux.ExceptWith(CapturedPieces(color));
        return aux;
    }
}