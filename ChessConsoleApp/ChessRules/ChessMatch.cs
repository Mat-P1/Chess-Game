using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;
using ChessConsoleApp.Chessboard.Exceptions;
using ChessConsoleApp.ChessRules.Pieces;

namespace ChessConsoleApp.ChessRules;

public class ChessMatch
{
    public GameBoard ChessMatchGameBoard { get; private set; }
    public int MatchTurn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool MatchFinished { get; private set; }
    private HashSet<Piece> _piecesOnTheBoard = new();
    private HashSet<Piece> _capturedPieces = new();
    public bool Check { get; private set; }

    public ChessMatch()
    {
        ChessMatchGameBoard = new GameBoard(8, 8);
        MatchTurn = 1;
        CurrentPlayer = Color.White;
        PlacePiecesOnBoard();
        MatchFinished = false;
        Check = false;
    }

    public void PlaceNewPiece(char column, int row, Piece piece)
    {
        ChessMatchGameBoard.PlacePiece(piece, new ChessPosition(column, row).ToArrayPosition());
        _piecesOnTheBoard.Add(piece);
    }
    
    private void PlacePiecesOnBoard()
    {
        // White Pieces
        PlaceNewPiece('a', 1, new Rook(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('b', 1, new Knight(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('c', 1, new Bishop(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('d', 1, new Queen(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('e', 1, new King(Color.White, ChessMatchGameBoard, this));
        PlaceNewPiece('f', 1, new Bishop(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('g', 1, new Knight(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('h', 1, new Rook(Color.White, ChessMatchGameBoard));
        
        PlaceNewPiece('a', 2, new Pawn(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('b', 2, new Pawn(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('c', 2, new Pawn(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('d', 2, new Pawn(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('e', 2, new Pawn(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('f', 2, new Pawn(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('g', 2, new Pawn(Color.White, ChessMatchGameBoard));
        PlaceNewPiece('h', 2, new Pawn(Color.White, ChessMatchGameBoard));
        
        // Black Pieces
        PlaceNewPiece('a', 8, new Rook(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('b', 8, new Knight(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('c', 8, new Bishop(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('d', 8, new Queen(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('e', 8, new King(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('f', 8, new Bishop(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('g', 8, new Knight(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('h', 8, new Rook(Color.Black, ChessMatchGameBoard));
        
        PlaceNewPiece('a', 7, new Pawn(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('b', 7, new Pawn(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('c', 7, new Pawn(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('d', 7, new Pawn(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('e', 7, new Pawn(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('f', 7, new Pawn(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('g', 7, new Pawn(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('h', 7, new Pawn(Color.Black, ChessMatchGameBoard));
    }

    public Piece? PieceMovement(Position origin, Position destination)
    {
        Piece pieceMove = ChessMatchGameBoard.RemovePiece(origin);
        pieceMove.IncrementsNumberOfMoves();
        Piece capturedPiece = ChessMatchGameBoard.RemovePiece(destination);
        ChessMatchGameBoard.PlacePiece(pieceMove, destination);
        
        if (capturedPiece != null)
        {
            _capturedPieces.Add(capturedPiece);
        }
        
        // Short Castling
        if (pieceMove is King && destination.ColumnPosition == origin.ColumnPosition + 2)
        {
            Position pieceOrigin = new Position(origin.RowPosition, origin.ColumnPosition + 3);
            Position pieceDestination = new Position(origin.RowPosition, origin.ColumnPosition + 1);
            Piece piece = ChessMatchGameBoard.RemovePiece(pieceOrigin);
            piece.IncrementsNumberOfMoves();
            ChessMatchGameBoard.PlacePiece(piece, pieceDestination);
        }
        
        // Long Castling
        if (pieceMove is King && destination.ColumnPosition == origin.ColumnPosition - 2)
        {
            Position pieceOrigin = new Position(origin.RowPosition, origin.ColumnPosition - 4);
            Position pieceDestination = new Position(origin.RowPosition, origin.ColumnPosition - 1);
            Piece piece = ChessMatchGameBoard.RemovePiece(pieceOrigin);
            piece.IncrementsNumberOfMoves();
            ChessMatchGameBoard.PlacePiece(piece, pieceDestination);
        }
        
        return capturedPiece;
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

    public void UndoMove(Position origin, Position destination, Piece capturedPiece)
    {
        Piece p = ChessMatchGameBoard.RemovePiece(destination);
        p.DecreaseNumberOfMoves();

        if (capturedPiece != null)
        {
            ChessMatchGameBoard.PlacePiece(capturedPiece, destination);
            _capturedPieces.Remove(capturedPiece);
        }
        ChessMatchGameBoard.PlacePiece(p, origin);
        
        // Short Castling
        if (p is King && destination.ColumnPosition == origin.ColumnPosition + 2)
        {
            Position pieceOrigin = new Position(origin.RowPosition, origin.ColumnPosition + 3);
            Position pieceDestination = new Position(origin.RowPosition, origin.ColumnPosition + 1);
            Piece piece = ChessMatchGameBoard.RemovePiece(pieceDestination);
            piece.DecreaseNumberOfMoves();
            ChessMatchGameBoard.PlacePiece(piece, pieceOrigin);
        }
        
        // Long Castling
        if (p is King && destination.ColumnPosition == origin.ColumnPosition - 2)
        {
            Position pieceOrigin = new Position(origin.RowPosition, origin.ColumnPosition - 4);
            Position pieceDestination = new Position(origin.RowPosition, origin.ColumnPosition - 1);
            Piece piece = ChessMatchGameBoard.RemovePiece(pieceDestination);
            piece.DecreaseNumberOfMoves();
            ChessMatchGameBoard.PlacePiece(piece, pieceOrigin);
        }
        
    }

    public void MakeAMove(Position origin, Position destination)
    {
        Piece? capturedPiece = PieceMovement(origin, destination);
        
        if (IsCheck(CurrentPlayer))
        {
            UndoMove(origin, destination, capturedPiece);
            throw new GameBoardExceptions("You cannot put yourself in check");
        }

        if (IsCheck(AdversaryPiece(CurrentPlayer)))
        {
            Check = true;
        }
        else
        {
            Check = false;
        }

        if (IsCheckMate(AdversaryPiece(CurrentPlayer)))
        {
            MatchFinished = true;
        }
        else
        {
            MatchTurn++;
            ChangePlayer();
        }
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

    private Color AdversaryPiece(Color color)
    {
        return color == Color.White ? Color.Black : Color.White;
    }

    private Piece? King(Color color)
    {
        foreach (Piece x in PiecesInGame(color))
        {
            if (x is King)
            {
                return x;
            }
        }
        return null;
    }

    public bool IsCheck(Color color)
    {
        Piece? k = King(color);
        if (k == null)
        {
            throw new GameBoardExceptions($"There's no {color} King on the board");
        }
        
        foreach (Piece x in PiecesInGame(AdversaryPiece(color)))
        {
            bool[,] possibleMoves = x.PossibleMoves();

            if (possibleMoves[k.PiecePosition.RowPosition, k.PiecePosition.ColumnPosition])
            {
                return true;
            }
        }
        return false;
    }

    public bool IsCheckMate(Color color)
    {
        if (!IsCheck(color))
        {
            return false;
        }

        foreach (Piece x in PiecesInGame(color))
        {
            bool[,] possibleMoves = x.PossibleMoves();
            for (int i = 0; i < ChessMatchGameBoard.GameBoardRows; i++)
            {
                for (int j = 0; j < ChessMatchGameBoard.GameBoardColumns; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        Position origin = x.PiecePosition;
                        Position target = new Position(i, j);
                        Piece capturedPiece = PieceMovement(origin, target);
                        bool checkTest = IsCheck(color);
                        UndoMove(origin, target, capturedPiece);

                        if (!checkTest)
                        {
                            return false;
                        }
                    }
                } 
            }
        }
        return true;
    }
}