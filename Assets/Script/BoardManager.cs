using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject PawnPrefab;
    //public GameObject KnightPrefab;
    //public GameObject RookPrefab;
    //public GameObject KingPrefab;

    public ChessPiece[,] board = new ChessPiece[7, 5];

    private void Start()
    {
        
    }

    void GenerateBoard()
    {
        for(int x=0; x<5; x++)
        {
            for(int z=0; z<7; z++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x, 0, z);
                tile.name = $"Tile {x},{z}";

                Tile t = tile.AddComponent<Tile>();
                t.boardPosition = new Vector2Int(x, z);
            }
        }
    }

    void SpawnPawn(int owner)
    {
        GameObject obj = Instantiate(PawnPrefab);
        obj.transform.position = new Vector3(0, 0.5f, 1);

        ChessPiece piece = obj.GetComponent<ChessPiece>();
        piece.SetPieceType(PieceType.Pawn);
        piece.SetOwner(owner);
        piece.boardPosition = new Vector2Int(0, 1);

        board[0, 1] = piece;

    }

    public void MovePiece(ChessPiece piece,Vector2Int target)
    {
        board[piece.boardPosition.x, piece.boardPosition.y] = null;

        piece.boardPosition = target;
        piece.transform.position = new Vector3(target.x, 0.5f, target.y);
        board[target.x, target.y] = piece;
    }
}
