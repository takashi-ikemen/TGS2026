using UnityEngine;

public class ForceMoveCard : Card
{
    public ForceMoveCard()
    {
        name = "ForceMoveCard";
    }

    public override GameState Use(GameState state)
    {
        //フラグを起動する
        state.IsForceMoveCardUsed = true;
        return state;
    }

    public override GameState Use(GameState state, int x, int y)
    {
        //3x3マスに空きますがあるか見る
        for(int i = x-1; i <= x + 1; i++)
        {
            //盤の外なら無視
            if (i < 0 || i > 4)
            {
                continue;
            }

            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if(j < 0 || j > 6)
                    {
                        continue;
                    }

                    //空きマスがあればフラグを起動
                    if (state.Board.Get(i, j).IsEmpty)
                    {
                        state.IsForceMovePieceChosen = true;
                        state.IsForceMoveCardUsed = false;
                        break;
                    }

                }
            }
            //二重for文から抜ける
            if (state.IsForceMovePieceChosen)
            {
                break;
            }

        }

        return state;
    }

    public override GameState Use(GameState state, int x1, int y1, int x2, int y2)
    {
        var newState = state;
        Move move = new Move(x1, y1, x2, y2);

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //移動させる
        newState = MoveApplier.Apply(newState, move);
       
        //カード使用中フラグを切る
        newState.IsForceMovePieceChosen = false;


        ////ターン交代
        //newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        return newState;

    }
}
