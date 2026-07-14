public enum PieceType { None, Pawn, Rook, Knight, King, Bishop }
public enum PieceColor { White, Black }

public struct Piece
{
    public PieceType Type; //Pieceの種類を格納するType
    public PieceColor Color;  //PieceColor(Black or White)を格納するColor

    public bool IsEmpty => Type == PieceType.None;

    public static Piece Empty => new Piece { Type = PieceType.None }; 
}