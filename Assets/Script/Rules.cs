using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public static class Rules
{
    public static List<Move> GetLegalMoves(GameState state)
    {
        //自駒の移動を見る
        var pseudo = MoveGenerator.GenerateMoves(state);
        var legal = new List<Move>();

        //１駒ずつ移動させてみる
        foreach (var move in pseudo)
        {
            var next = MoveApplier.Apply(state, move);

            //キングがチェックされない移動をlegalに格納
            if (!IsKingInCheck(next, state.Turn))
                legal.Add(move);
        }

        return legal;
    }

    static bool IsKingInCheck(GameState state, PieceColor color)
    {
        // あとで実装

        return false;
    }
}