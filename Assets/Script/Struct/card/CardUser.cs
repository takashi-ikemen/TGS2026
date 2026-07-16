using Unity.Mathematics;
using UnityEngine;

public class CardUser
{

    static public GameState ChooseUseCard(GameState state, string name)
    {
        int i = 0;
        var newState = state;
        if (state.Turn.Equals(PieceColor.White))//白のターン
        {
            while (i < state.WhiteCards.Count)//手札を１枚ずつ確認
            {
                if (state.WhiteCards[i].name.Equals(name))//nameが一致したら保留状態にする
                {
                    newState.OnHoldWhiteCard = state.WhiteCards[i];
                    newState.WhiteCards.RemoveAt(i);//手札から削除
                    break;
                }
                i++;
            }
        }
        else if(state.Turn.Equals(PieceColor.Black))//黒も同様に処理
        {
            while (i < state.BlackCards.Count)
            {
                if (state.BlackCards[i].name.Equals(name))
                {
                    newState.OnHoldBlackCard = state.BlackCards[i];
                    newState.BlackCards.RemoveAt(i);
                    break;
                }
                i++;
            }
        }


            return newState;
    }

    static public GameState UseCard(GameState state, string name)
    {
        var newState = state;

        //保留状態のカードを使用
        if (state.Turn.Equals(PieceColor.White))//白のターン
        {
            newState = state.OnHoldWhiteCard.Use(state);
        }
        else
        {
            newState = state.OnHoldBlackCard.Use(state);
        }
        return newState;
    }

    //座標を１つ指定するとき
    public GameState UseCard(GameState state, string name, int x, int y)
    {
        var newState = state;
        if (state.Turn.Equals(PieceColor.White))//白のターン
        {
            newState = state.OnHoldWhiteCard.Use(state, x, y);
        }
        else
        {
            newState = state.OnHoldBlackCard.Use(state, x, y);
        }
        return newState;
    }

    //座標を２つ指定するとき
    public GameState UseCard(GameState state, string name, int x1, int y1, int x2, int y2)
    {
        var newState = state;
        if (state.Turn.Equals(PieceColor.White))//白のターン
        {
            newState = state.OnHoldWhiteCard.Use(state, x1, y1, x2, y2);
        }
        else
        {
            newState = state.OnHoldBlackCard.Use(state, x1, y1, x2, y2);
        }
        return newState;
    }
}
