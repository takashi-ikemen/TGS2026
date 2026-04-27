public enum PieceType { None, Pawn, Rook, Knight, King }
public enum PieceColor { White, Black }

public struct Piece
{
    public PieceType Type;
    public PieceColor Color;

    public bool IsEmpty => Type == PieceType.None;

    public static Piece Empty => new Piece { Type = PieceType.None };
}