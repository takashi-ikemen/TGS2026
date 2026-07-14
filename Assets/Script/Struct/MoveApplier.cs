using System;
using System.Runtime.CompilerServices;

public static class MoveApplier
{
    public static GameState Apply(GameState state, Move move)
    {
        //現在のStateを別の変数newStateに格納
        var newState = state;

        //駒がオブジェクトに触れたかのイベント判定
        newState.TouchObject = false;

        //地雷が爆発したかのイベント判定
        newState.MineExploded = false;

        //聖杯が再配置されたかのイベント判定
        newState.GrailTake = false;

        var piece = newState.Board.Get(move.FromX, move.FromY);

        //移動先が地雷なら爆発
        if (move.ToX == newState.Mine.GetMineX() && move.ToY == newState.Mine.GetMineY() && newState.Mine.GetIsEnable() == true)
        {
            newState = MoveApplier.TouchMine(newState, move);
        }
        else if (move.ToX == newState.Grail.GetMineX() && move.ToY == newState.Grail.GetMineY())
        {

            newState = MoveApplier.TouchGrail(newState, move);
        }
        else
        {
            newState.Board.Set(move.ToX, move.ToY, piece);

            //RestrictMoveCardで変化した駒を元に戻す
            if (newState.Board.Get(move.ToX, move.ToY).Type.Equals(PieceType.Pawn))
            {
                newState.Board.Set(move.ToX, move.ToY, new Piece { Type = state.RestrictPieceType, Color = state.Board.Get(move.ToX, move.ToY).Color });
            }
        }


        newState.Board.Set(move.FromX, move.FromY, Piece.Empty);

        //EXTurnCardを使用していればターンを切り替えない
        if (state.IsWhiteEXTurn && state.Turn.Equals(PieceColor.White))
        {
            newState.IsWhiteEXTurn = false;
        }
        else if (state.IsBlackEXTurn && state.Turn.Equals(PieceColor.Black))
        {
            newState.IsBlackEXTurn = false;
        }
        else
        {
            //ターン交代
            newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }

        //聖杯が存在していなければオブジェクトを再生成
        if (newState.Grail.GetIsEnable() == false)
        {
            newState = MineGenerator.GenerateMine(0, 5, 0, 7, newState);
        }

        //BindX,Yを範囲外の値に設定
        newState.BindX = 999;
        newState.BindY = 999;

        return newState;
    }

    private static GameState TouchMine(GameState state , Move move)
    {
        var newState = state;
        var piece = state.Board.Get(move.FromX, move.FromY);
        CardDrawer cardDrawer = new CardDrawer();

        //地雷を踏んだら自分のHPを減らす
        if (piece.Color.Equals(PieceColor.Black))
        {
            newState.BlackHP = state.BlackHP - (2 + state.additionalMineDamage - state.BlackShield);//2ダメージ ＋ 追加ダメージ ― シールド
            newState.BlackShield = 0;//シールド消失

            if (!state.IsBlackUnbreakable)//破壊不能フラグがオフなら破壊
            {
                newState.Board.Set(move.ToX, move.ToY, Piece.Empty);
            }
            else//破壊不能フラグがオンならオフに
            {
                newState.IsBlackUnbreakable = false;
                newState.Board.Set(move.ToX, move.ToY, piece);
            }

            newState = cardDrawer.DrawCard(newState, PieceColor.Black);     //駒が破壊されたのでカードを引く
        }
        else if (piece.Color.Equals(PieceColor.White))
        {
            newState.WhiteHP = state.WhiteHP - (2 + state.additionalMineDamage - state.WhiteShield);//2ダメージ ＋ 追加ダメージ ― シールド
            newState.WhiteShield = 0;//シールド消失

            if (!state.IsWhiteUnbreakable)
            {
                newState.Board.Set(move.ToX, move.ToY, Piece.Empty);
            }
            else
            {
                newState.IsWhiteUnbreakable = false;
                newState.Board.Set(move.ToX, move.ToY, piece);
            }

            newState = cardDrawer.DrawCard(newState, PieceColor.White);     //駒が破壊されたのでカードを引く
        }

        newState.additionalMineDamage = 0; //地雷の追加ダメージを0に戻す

        newState.Mine.SetIsEnable(false);//地雷無効化

        newState.TouchObject = true;  //オブジェクトに触れた判定をとる
        newState.MineExploded = true;  //地雷が爆発したかの判定をとる

        //勝利判定
        GameFinisher gameFinisher = new GameFinisher();
        newState.Winner = gameFinisher.IsGameFinish(newState);

        return newState;

    }

    private static GameState TouchGrail(GameState state, Move move)
    {
        var newState = state;

        var piece = newState.Board.Get(move.FromX, move.FromY);
        newState.Board.Set(move.ToX, move.ToY, piece);

        newState.Grail.SetIsEnable(false);

        //RestrictMoveCardで変化した駒を元に戻す
        if (newState.Board.Get(move.ToX, move.ToY).Type.Equals(PieceType.Pawn))
        {
            newState.Board.Set(move.ToX, move.ToY, new Piece { Type = state.RestrictPieceType, Color = state.Board.Get(move.ToX, move.ToY).Color });
        }

        newState.TouchObject = true;  //オブジェクトに触れた判定をとる
        newState.GrailTake = true;  //聖杯を獲得した判定をとる


        //聖杯を取ったら相手のHPを1減らす
        if (state.Board.Get(state.Grail.GetMineX(), state.Grail.GetMineY()).Color.Equals(PieceColor.Black))
        {
            newState.WhiteHP = state.WhiteHP - (1 - state.WhiteShield);
            newState.WhiteShield = 0;
        }
        else
        {
            newState.BlackHP = state.BlackHP - (1 - state.BlackShield);
            newState.BlackShield = 0;
        }

        //勝利判定
        GameFinisher gameFinisher = new GameFinisher();
        newState.Winner = gameFinisher.IsGameFinish(newState);
        
        return newState;
    }

        
    
}