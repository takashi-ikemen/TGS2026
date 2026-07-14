using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState State;

    [SerializeField] private PieceViewManager pieceViewManager;
    [SerializeField] private ObjectViewManager objectViewManager;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CardViewManager cardViewManager;
    [SerializeField] private HPManager hpManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private FadeController fadeController;

    //前のStateを保持
    private GameState currentState;

    private void Start()
    {
        //フェードイン
        fadeController.FadeIn();

        //カーソルを表示・自由移動
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //初期stateの取得
        State = GameInitializer.CreateInitial();

        currentState = State;

        //SpawnObjectのためにいったんtureにする
        // State.GrailTake = true;

        cameraManager.ShowMainCamera(); //メインカメラに切り替え


        tileManager.Initialize();//タイルの配置
        pieceViewManager.Initialize(State);　　//駒の配置
        objectViewManager.Initialize(State);  //地雷・聖杯の配置
        uiManager.Initialize();    //HPバーの初期化

        uiManager.UpdateHP(State.WhiteHP, State.BlackHP);  //HPのUIを表示
        uiManager.UpdateTurn(State.Turn);  //ターンを表示
        hpManager.Initialize();


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

        //HPが変更されていたら
        if(currentState.BlackHP != State.BlackHP)
        {
            hpManager.UpdateHP(false, State.BlackHP);
        }

        if(currentState.WhiteHP != State.WhiteHP)
        {
            hpManager.UpdateHP(true, State.WhiteHP);
        }
     
        //勝利判定
        CheckWinner(State);

        //テスト用
        //cardViewManager.GenerateCard(State.WhiteCards);

        //currentStateの更新
        currentState = State;
       
    }

    /// <summary>
    /// 勝利判定を取る
    /// </summary>
    private void CheckWinner(GameState state)
    {
        if (state.WhiteHP <= 0)
        {
            Debug.Log("Black Win");
            sceneController.isWhiteWin = false;
            sceneController.SceneChange("ResultScene");
        }

        if (state.BlackHP <= 0)
        {
            Debug.Log("White Win");
            sceneController.isWhiteWin = true;
            sceneController.SceneChange("ResultScene");
        }
    }

    public List<Move> ViewCanMoveArea(int fx, int fy) //変数に指定した座標(x,y)に応じて,そこの座標に存在する駒が動ける範囲をすべて返す
    {
        var moveCheck = new List<Move>();
        moveCheck = MoveGenerator.GenerateMoves(State);

        List<Move> pieceMove = moveCheck.FindAll(move => move.FromX == fx && move.FromY == fy);
        return pieceMove;
    }

    public void GenerateCard()
    {
        if (State.WhiteCards.Count <= 0) return;
        cardViewManager.GenerateCard(State.WhiteCards);
    }

    public void  UseCard(CardView cardView)
    {

        Debug.Log("カード効果発動");
        string cardName = cardView.cardName;

        //カード効果使用後の変更を適用
        State = CardUser.UseCard(State, cardName);
        //カードを除外した後の変更を適用
        State = CardRemover.RemoveCard(State);

        cardViewManager.ClearCards();

        currentState = State;



    }
}