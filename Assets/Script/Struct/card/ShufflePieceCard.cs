using System;
using System.Collections.Generic;

public class ShufflePieceCard : Card
{
    public ShufflePieceCard()
    {
        name = "ShufflePieceCard";
    }

    public override GameState Use(GameState state)
    {
        Random random = new Random();
        int px, py;
        var newState = state;

        //残った駒の種類を保存
        List<Piece> pieceList = new List<Piece>();
        pieceList = CountPieces.SurvivePieceCounter(state);

        //盤面を初期化
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 7; y++)
                newState.Board.Squares[x, y] = Piece.Empty;

        //駒の再設置
        for(int i = 0; i < pieceList.Count; i++)
        {
            while (true)
            {
                px = random.Next(0, 5);
                py = random.Next(0, 7);
                if(newState.Board.Get(px, py).IsEmpty                                           //他の駒とかぶらない
                    && !(newState.Mine.GetMineX() == px && newState.Mine.GetMineY() == py)　       //地雷と被らない
                    && !(newState.Grail.GetMineX() == px && newState.Grail.GetMineY() == py))　    //聖杯と被らない
                {
                    //座標(px, py)に駒をセット
                    newState.Board.Set(px, py, pieceList[i]);
                    break;  //whileを抜ける
                }
            }
        }

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;



        return newState;
    }
}