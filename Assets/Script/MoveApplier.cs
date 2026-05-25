using System;
using System.Runtime.CompilerServices;

public static class MoveApplier
{
    public static GameState Apply(GameState state, Move move)
    {
        //現在のStateを別の変数newStateに格納
        var newState = state;

        //地雷が爆発したかの判定をとる
        newState.MineExploded = false;

        var piece = newState.Board.Get(move.FromX, move.FromY);

        //移動先が地雷なら爆発
        if (move.ToX == state.Mine.GetMineX() && move.ToY == state.Mine.GetMineY() && state.Mine.GetIsEnable() == true)
        {
            newState = MoveApplier.TouchMine(state, move);
        }
        else if (move.ToX == state.Grail.GetMineX() && move.ToY == state.Grail.GetMineY())
        {

            newState = MoveApplier.TouchGrail(state, move);
        }
        else
        {
            newState.Board.Set(move.ToX, move.ToY, piece);
        }

        newState.Board.Set(move.FromX, move.FromY, Piece.Empty);

        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

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

        newState.MineExploded = true;
        newState.Board.Set(move.ToX, move.ToY, Piece.Empty);
        return newState;

    }

    private static GameState TouchGrail(GameState state, Move move)
    {
        var newState = state;

        var piece = newState.Board.Get(move.FromX, move.FromY);
        newState.Board.Set(move.ToX, move.ToY, piece);

        //地雷再設置 0 <= x < 5 , 0<= y < 7
        newState = MineGenerator.GenerateMine(0, 5, 0, 7, state);

        //聖杯を取ったら相手のHPを1減らす
        if(state.Board.Get(state.Grail.GetMineX(),state.Grail.GetMineY()).Color.Equals(PieceColor.Black))
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