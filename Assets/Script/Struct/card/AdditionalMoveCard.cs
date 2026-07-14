using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class AdditionalMoveCard : Card
{
    public AdditionalMoveCard()
    {
        name = "AdditionalMoveCard";
    }

    public override GameState Use(GameState state)
    {
        var newState = state;

        //使用時にフラグをオンにする
        newState.IsAdditionalMoveCardUsed = true;

        //ターンの切り替え
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        return newState;
    }

    public override GameState Use(GameState state, int x, int y)
    {
        //3x3マスに空きますがあるか見る
        for (int i = x - 1; i <= x + 1; i++)
        {   
            //盤の外なら無視する
            if (i < 0 || i > 4)
            {
                continue;
            }
            
            for (int j = y - 1; j <= y + 1; j++)
                {
                    //盤の外なら無視する
                    if (j < 0 || j > 6)
                    {
                        continue;
                    }

                    //空きマスがあれば駒の選択を確定
                    if (state.Board.Get(i, j).IsEmpty)
                    {
                        state.IsAdditionalMovePieceChosen = true;
                        state.IsAdditionalMoveCardUsed = false;
                        break;
                    }

                }
            
            //二重for文から抜ける
            if (state.IsAdditionalMovePieceChosen)
            {
                break;
            }

        }

        return state;
    }

    public override GameState Use(GameState state, int x1, int y1, int x2, int y2)
    {
        var newState = state;

        Move move = new Move(x1,y1,x2,y2);

        //駒移動後のターン遷移を防ぐ
        if (state.Turn.Equals(PieceColor.White))
        {
            state.IsWhiteEXTurn = true;
        }
        else if(state.Turn.Equals(PieceColor.Black))
        {
            state.IsBlackEXTurn = true;
        }

        //移動処理
        newState = MoveApplier.Apply(state, move);

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //カード使用中フラグを切る
        newState.IsAdditionalMovePieceChosen = false;

        return newState;
    }

}
