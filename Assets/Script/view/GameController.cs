using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //盤の情報を保持するGameState
    GameState state;

    //フィールド全体が5*7のマス
    PieceView[,] views = new PieceView[5,7];

    //駒のPrefab
    [SerializeField] GameObject whitePawn;
    [SerializeField] GameObject blackPawn;
    [SerializeField] GameObject whiteRook;
    [SerializeField] GameObject blackRook;
    [SerializeField] GameObject whiteKnight;
    [SerializeField] GameObject blackKnight;
    [SerializeField] GameObject whiteKing;
    [SerializeField] GameObject blackKing;

    //Camera
    public Camera cam;
    //Layer
    public LayerMask pieceLayer;
    public LayerMask tileLayer;

    //マウスポインタが参照する駒currentHover
    PieceView currentHover;
    //クリック時に選択された駒selectedPiece
    PieceView selectedPiece;

    //マウスポインタが参照するタイルcurrentTile
    Tile currentTile;

    //タイルを管理するTileManager
    public TileManager tileManager;

    //選択された駒が動くことのできる範囲を保持するリストcurrentMoves
    List<Move> currentMoves;

    //駒を選択するかタイルを選択するかを切り替える変数isMoveRay
    bool isMoveRay = false;
    
    void Start()
    {
        //stateに最初のstateの状態を格納する
        state = GameInitializer.CreateInitial();

        Debug.Log(state.Turn);　//どちらのターンかをログに表示

        //駒を配置する
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
            var piece = state.Board.Get(x, y); //座標を指定し、Boardに格納されている駒の情報を入れる
   
            if (piece.IsEmpty) continue;　//駒がない(IsEmpty)のときはcontine

            var prefab = GetPrefab(piece); //駒の情報を参照し、それに対応したPrefabをとってくる
            var obj = Instantiate(prefab);　//Prefab生成

            var view = obj.GetComponent<PieceView>();
            view.SetPositionImmediate(x, y);  //初期位置に配置

            views[x, y] = view;　　//リストviewsにviewの情報を格納
        }
    }

    GameObject GetPrefab(Piece piece)
    {
        //引数に入れたPieceTypeによって、それに対応したPrefabを返す
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
     
    public void UpdateAreaPieceHover()　　　//マウスカーソルによって動かす駒を選択する
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


    public void UpdateAreaTileHover()　　//マウスカーソルによって移動先のタイルを選択する
    {
        if (currentMoves == null) return;  //currentMovesがnullの時は処理を無視する(errorが出ないようにするため)
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, tileLayer))
        {
            var tile = hit.collider.GetComponent<Tile>(); //Tileの情報を格納

            if(tile != null)
            {
                //カーソルの移動元の色をリセットして、移動先の方が光るようにする
                if(currentTile != null && currentTile != tile)
                {
                    currentTile.TileHighLight(Tile.HighLightTileType.None);
                }
                currentTile = tile;
                currentTile.TileHighLight(Tile.HighLightTileType.Hover);
            }
        }


        //タイルクリック時
        if(Input.GetMouseButtonDown(0))
        {
            Move selectedMove = default;
            bool found = false;
              
            //選択されたタイルが、選択された駒が動ける範囲(currentMoves)と同じならtrueを返す
            foreach(var move in currentMoves)
            {
                if(move.ToX == currentTile.tileX && move.ToY == currentTile.tileY)
                {
                    selectedMove = move;
                    found = true;
                    break;
                }
            }

            //↑合法手なら移動
            if (found)
            {
                //移動
                ApplyMove(selectedMove);
                Debug.Log("移動！");
            }

            //ハイライト解除
            tileManager.ClearHighLight();

            //currentTile,selectedPiece,currentMovesをリセットする
            currentTile = null;
            selectedPiece = null;
            currentMoves = null;

            //タイル選択モードを解除(isMoveRay  = false)
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

            //currentTile,selectedPiece,currentMovesをリセットする
            currentTile = null;
            selectedPiece = null;
            currentMoves = null;

            //タイル選択モードを解除(isMoveRay  = false)
            isMoveRay = false;
        }

        /*if (currentTile != null)
        {
            Debug.Log("ここですよー");
            currentTile.TileHighLight(Tile.HighLightTileType.None);
            currentTile = null;
        }*/
    }
        

    public List<Move> ViewCanMoveArea(int fx, int fy) //変数に指定した座標(x,y)に応じて,そこの座標に存在する駒が動ける範囲をすべて返す
    {
        var moveCheck = new List<Move>();
        moveCheck = MoveGenerator.GenerateMoves(state);

        List<Move> pieceMove = moveCheck.FindAll(move => move.FromX == fx && move.FromY == fy);
        return pieceMove;
    }

}