using System;
using System.Collections.Generic;
public class SwitchPieceCard : Card
{

    public SwitchPieceCard()
    {
        name = "SwitchPieceCard";
    }

    //座標選択前
    public override GameState Use(GameState state)
    {
        //フラグを起動
        state.IsSwitchPieceCardUsed = true;
        return state;
    }

    //座標選択後
    public override GameState Use(GameState state, int x1, int y1, int x2, int y2)
    {
        var newState = state;
        //引数から指定した駒２つを取得する
        Piece piece1 = state.Board.Get(x1, y1);
        Piece piece2 = state.Board.Get(x2, y2);

        //駒を入れ替える
        newState.Board.Set(x1, y1, piece2);
        newState.Board.Set(x2, y2, piece1);


        //カード使用中フラグを切る
        newState.IsSwitchPieceCardUsed = false;

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;



        return newState;
    }
}
