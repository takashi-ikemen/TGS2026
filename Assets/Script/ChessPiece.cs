using JetBrains.Annotations;
using UnityEngine;

public enum PieceType
{
    Pawn,
    Knight,
    Rook,
    King
}

public class ChessPiece : MonoBehaviour
{
    private int owner;
    private PieceType pieceType;
    public Vector2Int boardPosition;

    public ChessPiece(int owner,PieceType pieceType)
    {
        
    }

    public int GetOwner()
    {
        return this.owner;
    }

    public PieceType GetPieceType()
    {
        return this.pieceType;
    }

    public void SetOwner(int owner)
    {
        this.owner = owner;
    }
   public void SetPieceType(PieceType pieceType)
    {
        this.pieceType = pieceType;
    }




}
