using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DeugLog : MonoBehaviour
{
    GameState state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string boardStr = "";
        state = GameInitializer.CreateInitial();
        Piece piece = state.Board.Get(2,0);
        Mine mine = state.Mine;
        Debug.Log(piece.Type);
        Debug.Log(piece.Color);

        // 盤の出力
        for(int x=0; x < 5; x++) { 
            for(int y=0; y < 7; y++)
            {
                piece = state.Board.Get(x, y);
                boardStr += piece.Type;
            }
        boardStr += "\n";
        }
        Debug.Log(boardStr);

        Debug.Log($"mine:{mine.GetMineX()},{mine.GetMineY()}");

    }

    public void OnClickReroll()
    {
        Mine mine = MineGenerator.GenerateMine(0, 5, 0, 7, state.Board);
        Debug.Log($"mine:{mine.GetMineX()},{mine.GetMineY()}");

    } 

    public void OnClickMove()
    {
        //GenerateMovesの呼び出し
        var moveCheck = new List<Move>();
        moveCheck = MoveGenerator.GenerateMoves(state);

        //List<Move>の中身みる
        foreach (var item in moveCheck)
        {
            Debug.Log($"From:{item.FromX},{item.FromY}\nTo:{item.ToX},{item.ToY}");
        }

        //駒の位置を宣言↓
        int fx = 3;
        int fy = 0;

        //上記位置の駒のMoveを取得しListに格納
        List<Move> pieceMove = moveCheck.FindAll(move => move.FromX == fx && move.FromY == fy);
        foreach (var item in pieceMove)
        {
            Debug.Log($"From:{item.FromX},{item.FromY}\nTo:{item.ToX},{item.ToY}");
        }
        state = MoveApplier.Apply(state, pieceMove[1]);

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
        Debug.Log($"mine:{state.Mine.GetMineX()},{state.Mine.GetMineY()}");
    }
}
