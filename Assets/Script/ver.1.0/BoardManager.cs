using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject PawnPrefab;
    //public GameObject KnightPrefab;
    //public GameObject RookPrefab;
    //public GameObject KingPrefab;

    public ChessPiece[,] board = new ChessPiece[7, 5];
    private void Start()
    {
        GenerateBoard();
        SpawnPawn(1, 1, 1);
        SpawnPawn(1, 2, 1);
        SpawnPawn(1, 3, 1);
    }

    void GenerateBoard()
    {
        for(int x=0; x<5; x++)
        {
            for(int z=0; z<7; z++)
            {
                GameObject tile = Instantiate(TilePrefab);
                tile.transform.position = new Vector3(x, 0, z);
                tile.name = $"Tile {x},{z}";

                Tile t = tile.AddComponent<Tile>();
                t.boardPosition = new Vector2Int(x, z);
            }
        }
    }
   

    void SpawnPawn(int owner, float positionX, float  positionZ)
    {
        GameObject obj = Instantiate(PawnPrefab);
        obj.transform.position = new Vector3(positionX, 0.5f, positionZ);

        ChessPiece piece = obj.GetComponent<ChessPiece>();
        piece.SetPieceType(PieceType.Pawn);
        piece.SetOwner(owner);
        piece.boardPosition = new Vector2Int((int)positionX, (int)positionZ);
         
        board[(int)positionX, (int)positionZ] = piece;
        Debug.Log($"pawn {positionX},{positionZ}");
    }

    //void SpawnKnight(int owner)
    //{
    //    GameObject obj = Instantiate(KnightPrefab);
    //    obj.transform.position = new Vector3(3, 0.5f, 0);

    //    ChessPiece piece = obj.GetComponent<ChessPiece>();
    //    piece.SetPieceType(PieceType.Knight);
    //    piece.SetOwner(owner);
    //    piece.boardPosition = new Vector2Int(0, 1);

    //    board[3, 0] = piece;

    //}



    public void MovePiece(ChessPiece piece,Vector2Int target)
    {
        board[piece.boardPosition.x, piece.boardPosition.y] = null;

        piece.boardPosition = target;
        piece.transform.position = new Vector3(target.x, 0.5f, target.y);
        board[target.x, target.y] = piece;
    }
}
