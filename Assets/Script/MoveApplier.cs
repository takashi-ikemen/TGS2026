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
        }

        newState.Board.Set(move.FromX, move.FromY, Piece.Empty);

        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        //聖杯が存在していなければオブジェクトを再生成
        if (newState.Grail.GetIsEnable() == false)
        {
            newState = MineGenerator.GenerateMine(0, 5, 0, 7, newState);
        }

        return newState;
    }

    private static GameState TouchMine(GameState state , Move move)
    {
        var newState = state;

        //地雷を踏んだら自分のHPを2減らす
        if(state.Board.Get(state.Mine.GetMineX(), state.Mine.GetMineY()).Color.Equals(PieceColor.Black))
        {
            newState.BlackHP = state.BlackHP - 2;
        }
        else
        {
            newState.WhiteHP = state.WhiteHP - 2;
        }

        newState.Mine.SetIsEnable(false);

        newState.TouchObject = true;  //オブジェクトに触れた判定をとる
        newState.MineExploded = true;  //地雷が爆発したかの判定をとる
        newState.Board.Set(move.ToX, move.ToY, Piece.Empty);
        return newState;

    }

    private static GameState TouchGrail(GameState state, Move move)
    {
        var newState = state;

        var piece = newState.Board.Get(move.FromX, move.FromY);
        newState.Board.Set(move.ToX, move.ToY, piece);

        newState.Grail.SetIsEnable(false);

        newState.TouchObject = true;  //オブジェクトに触れた判定をとる
        newState.GrailTake = true;  //聖杯を獲得した判定をとる


        //聖杯を取ったら相手のHPを1減らす
        if (state.Board.Get(state.Grail.GetMineX(),state.Grail.GetMineY()).Color.Equals(PieceColor.Black))
        {
            newState.WhiteHP = state.WhiteHP - 1;
        }
        else
        {
            newState.BlackHP = state.BlackHP - 1;
        }

        return newState;
    }

        
    
}