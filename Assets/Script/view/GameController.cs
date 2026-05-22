using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameState state;

    PieceView[,] views = new PieceView[5,7];

    [SerializeField] GameObject whitePawn;
    [SerializeField] GameObject blackPawn;
    [SerializeField] GameObject whiteRook;
    [SerializeField] GameObject blackRook;
    [SerializeField] GameObject whiteKnight;
    [SerializeField] GameObject blackKnight;
    [SerializeField] GameObject whiteKing;
    [SerializeField] GameObject blackKing;

    public Camera cam;
    public LayerMask pieceLayer;
    public LayerMask tileLayer;

    PieceView currentHover;
    PieceView selectedPiece;
    Tile currentTile;
    public TileManager tileManager;
    List<Move> currentMoves;

    bool isMoveRay = false;
    
    void Start()
    {
        state = GameInitializer.CreateInitial();
        Debug.Log(state.Turn);
        SpawnPieces();
  
    }

    private void Update()
    {
        if (!isMoveRay)
        {
            UpdateAreaPieceHover();
        }
        else
        {
            UpdateAreaTileHover();
        }
    }

    void SpawnPieces()
    {
        for (int x = 0; x < 5; x++)
        for (int y = 0; y < 7; y++)
        {
            var piece = state.Board.Get(x, y);
                Debug.Log(piece.Type);
            if (piece.IsEmpty) continue;

            var prefab = GetPrefab(piece);
            var obj = Instantiate(prefab);

            var view = obj.GetComponent<PieceView>();
            view.SetPositionImmediate(x, y);

            views[x, y] = view;
        }
    }

    GameObject GetPrefab(Piece piece)
    {
        Debug.Log(piece.Type);
        if (piece.Type == PieceType.Pawn)
            return piece.Color == PieceColor.White ? whitePawn : blackPawn;

        if (piece.Type == PieceType.Rook)
            return piece.Color == PieceColor.White ? whiteRook : blackRook;

        if (piece.Type == PieceType.Knight)
            return piece.Color == PieceColor.White ? whiteKnight : blackKnight;

        if (piece.Type == PieceType.King)
            return piece.Color == PieceColor.White ? whiteKing : blackKing;

        return null;
    }


    public void ApplyMove(Move move)
    {
        // Core更新
        state = MoveApplier.Apply(state, move);

        // --- View更新 ---
        var view = views[move.FromX, move.FromY];

        // 取る処理
        if (views[move.ToX, move.ToY] != null)
        {
            Destroy(views[move.ToX, move.ToY].gameObject);
        }

        views[move.FromX, move.FromY] = null;

        //地雷爆発
        if (state.MineExploded)
        {
            //移動駒削除
            Destroy(view.gameObject);

            //爆発エフェクト
            Debug.Log("爆発！！！");

            return;
        }

        //通常移動
        view.MoveTo(move.ToX, move.ToY);
        views[move.ToX, move.ToY] = view;
    }
     
    public void UpdateAreaPieceHover()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        TileManager tileM = tileManager;

        if(Physics.Raycast(ray,out RaycastHit hit,100f,pieceLayer))
        {
            var piece = hit.collider.GetComponent<PieceView>();

            //選択している駒のハイライトを表示
            if(piece != null)
            {
                //前のハイライトを戻す
                if(currentHover != null && currentHover != piece)
                {
                    currentHover.SetHighLight(PieceView.HighLightType.None);
                }
                currentHover = piece;
                currentHover.SetHighLight(PieceView.HighLightType.Hover);

                //駒をクリックしているときに移動できる範囲を表示する
                if (Input.GetMouseButton(0))
                {
                    selectedPiece = piece;

                    currentHover.SetHighLight(PieceView.HighLightType.Selected);


                    currentMoves = ViewCanMoveArea(piece.x, piece.y);
                    
                    tileM.ViewArea(selectedPiece.x, selectedPiece.y);

                        
                    

                    isMoveRay = true;
                }
                else if (Input.GetMouseButton(2))
                {
                    //tileM.ViewArea(99, 99);
                    Debug.Log("dame");
                }

                return;
            }
        }

        //何もあたっていないとき
        /*if(currentHover != null)
        {
            currentHover.SetHighLight(PieceView.HighLightType.None);
            currentHover = null;
        }*/
    }


    public void UpdateAreaTileHover()
    {
        if (currentMoves == null)
        {
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, tileLayer))
        {
            var tile = hit.collider.GetComponent<Tile>();

            if(tile != null)
            {
                if(currentTile != null && currentTile != tile)
                {
                    currentTile.TileHighLight(Tile.HighLightTileType.None);
                }
                currentTile = tile;
                currentTile.TileHighLight(Tile.HighLightTileType.Hover);
            }
        }


        //クリック時
        if(Input.GetMouseButtonDown(0))
        {
            Move selectedMove = default;
            bool found = false;
              
            foreach(var move in currentMoves)
            {
                if(move.ToX == currentTile.tileX && move.ToY == currentTile.tileY)
                {
                    selectedMove = move;
                    found = true;
                    break;
                }
            }

            //合法手なら移動
            if (found)
            {
                //移動
                ApplyMove(selectedMove);
                Debug.Log("移動！");
            }

            //ハイライト解除
            tileManager.ClearHighLight();


            currentTile = null;
            selectedPiece = null;
            currentMoves = null;

            isMoveRay = false;
        }

            //右クリックキャンセル
        if (Input.GetMouseButtonDown(1))
        {   
            tileManager.ClearHighLight();

            if(currentTile != null)
            {
                currentTile.TileHighLight(Tile.HighLightTileType.None);
            }

            currentTile = null;
            selectedPiece = null;
            currentMoves = null;

            isMoveRay = false;
        }

        /*if (currentTile != null)
        {
            Debug.Log("ここですよー");
            currentTile.TileHighLight(Tile.HighLightTileType.None);
            currentTile = null;
        }*/
    }
        

    public List<Move> ViewCanMoveArea(int fx, int fy)
    {
        var moveCheck = new List<Move>();
        moveCheck = MoveGenerator.GenerateMoves(state);

        List<Move> pieceMove = moveCheck.FindAll(move => move.FromX == fx && move.FromY == fy);
        return pieceMove;
    }

}