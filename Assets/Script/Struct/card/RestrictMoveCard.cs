using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class RestrictMoveCard : Card
{
    public RestrictMoveCard()
    {
        name = "RestrictMoveCard";
    }

    public override GameState Use(GameState state)
    {
        //フラグを起動
        state.IsRestrictMoveCardUsed = true;

        return state;
    }

    public override GameState Use(GameState state, int x, int y)
    {
        //対象の駒のPieceTypeを保存
        state.RestrictPieceType = state.Board.Get(x, y).Type;

        //移動方向を指定する配列
        int[] dx = { 1, 0, -1, 0 };
        int[] dy = { 0, -1, 0, 1 };

        //空きマスがあればフラグを起動
        for(int i = 0; i < 4; i++)
        {
            //指定できるマスの座標
            int nx = x + dx[i];
            int ny = y + dy[i];
            if (state.Board.Get(nx, ny).IsEmpty)
            {
                //空きマスがあればフラグを起動し、駒を置き換える
                state.Board.Set(x, y, new Piece { Type = PieceType.Pawn, Color = state.Board.Get(x,y).Color});
                state.IsRestrictMovePieceChosen = true;
                state.IsRestrictMoveCardUsed = false;
                break;
            }
        }

        return state;
    }

    public override GameState Use(GameState state, int x1, int y1, int x2, int y2)
    {
        var newState = state;
        
        //Move型で使える形にして保存
        newState.RestrictX = x2 - x1;
        newState.RestrictY = y2 - y1;

        newState.IsRestrictMovePieceChosen = false;

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        return newState;
    }

}
