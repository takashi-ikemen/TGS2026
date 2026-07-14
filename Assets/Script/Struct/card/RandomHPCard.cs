using System;
using UnityEngine.InputSystem.LowLevel;

public class RandomHPCard : Card
{
    public RandomHPCard()
    {
        name = "RandomHPCard";
    }

    public override GameState Use(GameState state)
    {
        Random random = new Random();
        int upDown = random.Next(0, 2);//回復かダメージかをランダムに決定、0なら回復 1ならダメージ
        var newState = state;

        if (state.Turn.Equals(PieceColor.White))//白が使ったとき
        {
            if(upDown == 0)
            {
                newState.WhiteHP = state.WhiteHP + 1;//回復
            }
            else
            {
                newState.WhiteHP = state.WhiteHP - 1;//ダメージ
            }
        }
        else//黒が使ったとき
        {
            if(upDown == 0)
            {
                newState.BlackHP = state.BlackHP + 1;//回復
            }
            else
            {
                newState.BlackHP = state.BlackHP - 1;//ダメージ
            }
        }

        //勝利判定
        GameFinisher gameFinisher = new GameFinisher();
        newState.Winner = gameFinisher.IsGameFinish(newState);

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;



        return newState;
    }
}
