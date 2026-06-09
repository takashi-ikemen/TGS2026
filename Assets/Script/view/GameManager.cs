using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState State;

    [SerializeField] private PieceViewManager pieceViewManager;
    [SerializeField] private ObjectViewManager objectViewManager;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        //初期stateの取得
        State = GameInitializer.CreateInitial();

        //SpawnObjectのためにいったんtureにする
        // State.GrailTake = true;


        tileManager.Initialize();//タイルの配置
        pieceViewManager.Initialize(State);　　//駒の配置
        objectViewManager.Initialize(State);  //地雷・聖杯の配置
        uiManager.Initialize();    //HPバーの初期化

        uiManager.UpdateHP(State.WhiteHP, State.BlackHP);  //HPのUIを表示
        uiManager.UpdateTurn(State.Turn);  //ターンを表示

        State.GrailTake = false; //オブジェクトが取られていない状態に戻す
    }

    /// <summary>
    /// struct側の変更をView上に適用
    /// </summary>
    public void ApplyMove(Move move)
    {
        //変更適用後のstateを取得
        State = MoveApplier.Apply(State, move);

        //PieceViewで変更を適用
        pieceViewManager.ApplyMove(move, State);
        //ObjectManagerで変更を適用 → オブジェクトの判定をとる
        objectViewManager.UpdateObjects(State);

        //UIで変更を適用
        uiManager.UpdateHP(State.WhiteHP, State.BlackHP);
        uiManager.UpdateTurn(State.Turn);

        //勝利判定
        CheckWinner(State);
    }

    /// <summary>
    /// 勝利判定を取る
    /// </summary>
    private void CheckWinner(GameState state)
    {
        if (state.WhiteHP <= 0)
        {
            Debug.Log("Black Win");
        }

        if (state.BlackHP <= 0)
        {
            Debug.Log("White Win");
        }
    }

    public List<Move> ViewCanMoveArea(int fx, int fy) //変数に指定した座標(x,y)に応じて,そこの座標に存在する駒が動ける範囲をすべて返す
    {
        var moveCheck = new List<Move>();
        moveCheck = MoveGenerator.GenerateMoves(State);

        List<Move> pieceMove = moveCheck.FindAll(move => move.FromX == fx && move.FromY == fy);
        return pieceMove;
    }
}