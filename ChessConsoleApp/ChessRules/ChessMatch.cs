using ChessConsoleApp.Chessboard;
using ChessConsoleApp.Chessboard.Enumerations;
using ChessConsoleApp.Chessboard.Exceptions;
using ChessConsoleApp.ChessRules.Pieces;

namespace ChessConsoleApp.ChessRules;

public class ChessMatch
{
    public GameBoard ChessMatchGameBoard { get; }
    public int MatchTurn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool MatchFinished { get; private set; }
    public bool Check { get; private set; }
    public Piece? VulnerableEnPassant { get; private set; }
    private readonly HashSet<Piece> _capturedPieces = new();
    private readonly HashSet<Piece> _piecesOnTheBoard = new();

    public ChessMatch()
    {
        ChessMatchGameBoard = new GameBoard(8, 8);
        MatchTurn = 1;
        CurrentPlayer = Color.White;
        MatchFinished = false;
        Check = false;
        VulnerableEnPassant = null;
        PlacePiecesOnBoard();
    }

    public void ValidateOriginPosition(Position position)
    {
        if (ChessMatchGameBoard.ReturnPiecePosition(position) == null)
            throw new GameBoardExceptions("There is no piece on this position");

        if (CurrentPlayer != ChessMatchGameBoard.ReturnPiecePosition(position).PieceColor)
            throw new GameBoardExceptions("The chosen piece is not yours");

        if (!ChessMatchGameBoard.ReturnPiecePosition(position).IsThereAnyPossibleMove())
            throw new GameBoardExceptions("There is no possible moves for the chosen piece");
    }

    public void ValidateTargetPosition(Position origin, Position destination)
    {
        if (!ChessMatchGameBoard.ReturnPiecePosition(origin).CanPieceMoveTo(destination))
            throw new GameBoardExceptions("The chosen piece can't move to target position");
    }
    
    public void MakeAMove(Position origin, Position destination)
    {
        var capturedPiece = PieceMovement(origin, destination);

        if (IsCheck(CurrentPlayer))
        {
            UndoMove(origin, destination, capturedPiece);
            throw new GameBoardExceptions("You cannot put yourself in check");
        }

        var piece = ChessMatchGameBoard.ReturnPiecePosition(destination);

        // Pawn Promotion to Queen
        if (piece is Pawn)
            if ((piece.PieceColor == Color.White && destination.RowPosition == 0) ||
                (piece.PieceColor == Color.Black && destination.RowPosition == 7))
            {
                piece = ChessMatchGameBoard.RemovePiece(destination);
                _piecesOnTheBoard.Remove(piece);
                Piece queen = new Queen(piece.PieceColor, ChessMatchGameBoard);
                ChessMatchGameBoard.PlacePiece(queen, destination);
                _piecesOnTheBoard.Add(queen);
            }

        Check = IsCheck(AdversaryPiece(CurrentPlayer));

        if (IsCheckMate(AdversaryPiece(CurrentPlayer)))
        {
            MatchFinished = true;
        }
        else
        {
            MatchTurn++;
            ChangePlayer();
        }

        // En Passant

        if (piece is Pawn && (destination.RowPosition == origin.RowPosition - 2 ||
                              destination.RowPosition == origin.RowPosition + 2))
            VulnerableEnPassant = piece;
        else
            VulnerableEnPassant = null;
    }

    public HashSet<Piece> CapturedPieces(Color color)
    {
        var aux = new HashSet<Piece>();

        foreach (var x in _capturedPieces)
            if (x.PieceColor == color)
                aux.Add(x);
        return aux;
    }
    
    private void PlaceNewPiece(char column, int row, Piece piece)
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

        PlaceNewPiece('a', 2, new Pawn(Color.White, ChessMatchGameBoard, this));
        PlaceNewPiece('b', 2, new Pawn(Color.White, ChessMatchGameBoard, this));
        PlaceNewPiece('c', 2, new Pawn(Color.White, ChessMatchGameBoard, this));
        PlaceNewPiece('d', 2, new Pawn(Color.White, ChessMatchGameBoard, this));
        PlaceNewPiece('e', 2, new Pawn(Color.White, ChessMatchGameBoard, this));
        PlaceNewPiece('f', 2, new Pawn(Color.White, ChessMatchGameBoard, this));
        PlaceNewPiece('g', 2, new Pawn(Color.White, ChessMatchGameBoard, this));
        PlaceNewPiece('h', 2, new Pawn(Color.White, ChessMatchGameBoard, this));

        // Black Pieces
        PlaceNewPiece('a', 8, new Rook(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('b', 8, new Knight(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('c', 8, new Bishop(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('d', 8, new Queen(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('e', 8, new King(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('f', 8, new Bishop(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('g', 8, new Knight(Color.Black, ChessMatchGameBoard));
        PlaceNewPiece('h', 8, new Rook(Color.Black, ChessMatchGameBoard));

        PlaceNewPiece('a', 7, new Pawn(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('b', 7, new Pawn(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('c', 7, new Pawn(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('d', 7, new Pawn(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('e', 7, new Pawn(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('f', 7, new Pawn(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('g', 7, new Pawn(Color.Black, ChessMatchGameBoard, this));
        PlaceNewPiece('h', 7, new Pawn(Color.Black, ChessMatchGameBoard, this));
    }

    private Piece PieceMovement(Position origin, Position destination)
    {
        var pieceMove = ChessMatchGameBoard.RemovePiece(origin);
        pieceMove.IncrementsNumberOfMoves();
        var capturedPiece = ChessMatchGameBoard.RemovePiece(destination);
        ChessMatchGameBoard.PlacePiece(pieceMove, destination);

        if (capturedPiece != null) _capturedPieces.Add(capturedPiece);

        // Short Castling
        if (pieceMove is King && destination.ColumnPosition == origin.ColumnPosition + 2)
        {
            var pieceOrigin = new Position(origin.RowPosition, origin.ColumnPosition + 3);
            var pieceDestination = new Position(origin.RowPosition, origin.ColumnPosition + 1);
            var piece = ChessMatchGameBoard.RemovePiece(pieceOrigin);
            piece.IncrementsNumberOfMoves();
            ChessMatchGameBoard.PlacePiece(piece, pieceDestination);
        }

        // Long Castling
        if (pieceMove is King && destination.ColumnPosition == origin.ColumnPosition - 2)
        {
            var pieceOrigin = new Position(origin.RowPosition, origin.ColumnPosition - 4);
            var pieceDestination = new Position(origin.RowPosition, origin.ColumnPosition - 1);
            var piece = ChessMatchGameBoard.RemovePiece(pieceOrigin);
            piece.IncrementsNumberOfMoves();
            ChessMatchGameBoard.PlacePiece(piece, pieceDestination);
        }

        // En Passant
        if (pieceMove is Pawn)
            if (origin.ColumnPosition != destination.ColumnPosition && capturedPiece == null)
            {
                var pawnPosition = pieceMove.PieceColor == Color.White 
                    ? new Position(destination.RowPosition + 1, destination.ColumnPosition) 
                    : new Position(destination.RowPosition - 1, destination.ColumnPosition);

                capturedPiece = ChessMatchGameBoard.RemovePiece(pawnPosition);
                _capturedPieces.Add(capturedPiece);
            }

        return capturedPiece!;
    }
    
    private void ChangePlayer()
    {
        CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
    }

    private void UndoMove(Position origin, Position destination, Piece capturedPiece)
    {
        var pieceUndo = ChessMatchGameBoard.RemovePiece(destination);
        pieceUndo.DecreaseNumberOfMoves();

        if (capturedPiece != null)
        {
            ChessMatchGameBoard.PlacePiece(capturedPiece, destination);
            _capturedPieces.Remove(capturedPiece);
        }

        ChessMatchGameBoard.PlacePiece(pieceUndo, origin);

        // Short Castling
        if (pieceUndo is King && destination.ColumnPosition == origin.ColumnPosition + 2)
        {
            var pieceOrigin = new Position(origin.RowPosition, origin.ColumnPosition + 3);
            var pieceDestination = new Position(origin.RowPosition, origin.ColumnPosition + 1);
            var piece = ChessMatchGameBoard.RemovePiece(pieceDestination);
            piece.DecreaseNumberOfMoves();
            ChessMatchGameBoard.PlacePiece(piece, pieceOrigin);
        }

        // Long Castling
        if (pieceUndo is King && destination.ColumnPosition == origin.ColumnPosition - 2)
        {
            var pieceOrigin = new Position(origin.RowPosition, origin.ColumnPosition - 4);
            var pieceDestination = new Position(origin.RowPosition, origin.ColumnPosition - 1);
            var piece = ChessMatchGameBoard.RemovePiece(pieceDestination);
            piece.DecreaseNumberOfMoves();
            ChessMatchGameBoard.PlacePiece(piece, pieceOrigin);
        }

        // En Passant
        if (pieceUndo is Pawn)
            if (origin.ColumnPosition != destination.ColumnPosition && capturedPiece == VulnerableEnPassant)
            {
                var pawn = ChessMatchGameBoard.RemovePiece(destination);
                var pawnPosition = pieceUndo.PieceColor == Color.White ? new Position(3, destination.ColumnPosition) 
                    : new Position(4, destination.ColumnPosition);
                ChessMatchGameBoard.PlacePiece(pawn, pawnPosition);
            }
    }

    private HashSet<Piece> PiecesInGame(Color color)
    {
        var aux = new HashSet<Piece>();

        foreach (var x in _piecesOnTheBoard)
            if (x.PieceColor == color)
                aux.Add(x);
        aux.ExceptWith(CapturedPieces(color));
        return aux;
    }

    private Color AdversaryPiece(Color color)
    {
        return color == Color.White ? Color.Black : Color.White;
    }

    private Piece? King(Color color)
    {
        foreach (var x in PiecesInGame(color))
            if (x is King)
                return x;
        return null;
    }

    private bool IsCheck(Color color)
    {
        var k = King(color);
        if (k == null) throw new GameBoardExceptions($"There's no {color} King on the board");

        foreach (var x in PiecesInGame(AdversaryPiece(color)))
        {
            var possibleMoves = x.PossibleMoves();

            if (possibleMoves[k.PiecePosition.RowPosition, k.PiecePosition.ColumnPosition]) return true;
        }

        return false;
    }

    private bool IsCheckMate(Color color)
    {
        if (!IsCheck(color)) return false;

        foreach (var x in PiecesInGame(color))
        {
            var possibleMoves = x.PossibleMoves();
            for (var i = 0; i < ChessMatchGameBoard.GameBoardRows; i++)
            for (var j = 0; j < ChessMatchGameBoard.GameBoardColumns; j++)
                if (possibleMoves[i, j])
                {
                    var origin = x.PiecePosition;
                    var target = new Position(i, j);
                    var capturedPiece = PieceMovement(origin, target);
                    var checkTest = IsCheck(color);
                    UndoMove(origin, target, capturedPiece);

                    if (!checkTest) return false;
                }
        }

        return true;
    }
}