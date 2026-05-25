using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem.LowLevel;


public class DeugLog : MonoBehaviour
{
    GameState state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string boardStr = "";
        state = GameInitializer.CreateInitial();
        Piece piece = state.Board.Get(2, 0);
        var grail = state.Grail;
        var mine = state.Mine;
        Debug.Log(piece.Type);
        Debug.Log(piece.Color);

        // 盤の出力
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                piece = state.Board.Get(x, y);
                boardStr += piece.Type;
            }
            boardStr += "\n";
        }
        Debug.Log(boardStr);

        Debug.Log($"grail:{grail.GetMineX()},{grail.GetMineY()}, {grail.GetIsEnable()},{grail.GetIsVisible()}");
        Debug.Log($"mine:{mine.GetMineX()},{mine.GetMineY()}, {mine.GetIsEnable()},{mine.GetIsVisible()}");

    }

    public void OnClickReroll()
    {
        state = MineGenerator.GenerateMine(0, 5, 0, 7, state);
        Debug.Log($"grail:{state.Grail.GetMineX()},{state.Grail.GetMineY()}, {state.Grail.GetIsEnable()},{state.Grail.GetIsVisible()}");
        Debug.Log($"mine:{state.Mine.GetMineX()},{state.Mine.GetMineY()}, {state.Mine.GetIsEnable()},{state.Mine.GetIsVisible()}");

    }

    public void OnClickMove()
    {

        //Debug.Log($"前前前grail:{state.Grail.GetMineX()},{state.Grail.GetMineY()}, {state.Grail.GetIsGrail()},{state.Grail.GetIsVisible()}");
        //Debug.Log($"前前前mine:{state.Mine.GetMineX()},{state.Mine.GetMineY()}, {state.Mine.GetIsGrail()},{state.Mine.GetIsVisible()}");

        //GenerateMovesの呼び出し
        var moveCheck = new List<Move>();
        moveCheck = MoveGenerator.GenerateMoves(state);

        //Debug.Log($"前前grail:{state.Grail.GetMineX()} , {state.Grail.GetMineY()} ,  {state.Grail.GetIsGrail()} , {state.Grail.GetIsVisible()}");
        //Debug.Log($"前前mine:{state.Mine.GetMineX()},{state.Mine.GetMineY()}, {state.Mine.GetIsGrail()} , {state.Mine.GetIsVisible()}");

        //List<Move>の中身みる
        foreach (var item in moveCheck)
        {
            Debug.Log($"From:{item.FromX},{item.FromY}\nTo:{item.ToX},{item.ToY}");
        }

        //駒の位置を宣言↓
        int fx = 1;
        int fy = 0;

        //上記位置の駒の移動可能パターンを取得しListに格納
        List<Move> pieceMove = moveCheck.FindAll(move => move.FromX == fx && move.FromY == fy);
        foreach (var item in pieceMove)
        {
            Debug.Log($"From:{item.FromX},{item.FromY}\nTo:{item.ToX},{item.ToY}");
        }

        //Debug.Log(pieceMove[0]);

        Debug.Log($"前grail:{state.Grail.GetMineX()} , {state.Grail.GetMineY()} ,  {state.Grail.GetIsEnable()} , {state.Grail.GetIsVisible()}");
        Debug.Log($"前mine:{state.Mine.GetMineX()} , {state.Mine.GetMineY()}, {state.Mine.GetIsEnable()},{state.Mine.GetIsVisible()}");

        //移動可能パターンから選んだ移動を適用
        state = MoveApplier.Apply(state, moveCheck[5]);


        //盤の出力
        string boardStr = "";
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                Piece piece = state.Board.Get(x, y);
                boardStr += piece.Type;
            }
            boardStr += "\n";
        }
        Debug.Log(boardStr);
        Debug.Log($"今grail:{state.Grail.GetMineX()} , {state.Grail.GetMineY()} ,  {state.Grail.GetIsEnable()} , {state.Grail.GetIsVisible()}");
        Debug.Log($"今mine:{state.Mine.GetMineX()} , {state.Mine.GetMineY()}, {state.Mine.GetIsEnable()},{state.Mine.GetIsVisible()}");
        Debug.Log($"BlackHP:{state.BlackHP},WhiteHP:{state.WhiteHP}");
    }

    public void OnClickApocalypse()
    {
        state.Board.Set(2, 6, Piece.Empty);
        //state.Board.Set(1,5, Piece.Empty);
        //state.Board.Set(2,5, Piece.Empty);
        //state.Board.Set(3,6, Piece.Empty);
        //state.Board.Set(3,5, Piece.Empty);

        //破壊後の盤
        string boardStr = "";
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                Piece piece = state.Board.Get(x, y);
                boardStr += piece.Type;
            }
            boardStr += "\n";
        }
        Debug.Log(boardStr);
    }

    public void OnClickJudge()
    {
        //ゲーム続行か終了か
        GameFinisher gameFinisher = new GameFinisher();
        Winner winner = gameFinisher.IsGameFinish(state);

        //生存コマ確認
        List<Piece> surviveBlackPieces = CountPieces.SurviveBlackPieceCounter(state);
        foreach (var item in surviveBlackPieces)
        {
            Debug.Log($"Black:{item.Type}");
        }
        List<Piece> surviveWhitePieces = CountPieces.SurviveWhitePieceCounter(state);
        foreach (var item in surviveWhitePieces)
        {
            Debug.Log($"White:{item.Type}");
        }

        Debug.Log(winner);

    }
}
