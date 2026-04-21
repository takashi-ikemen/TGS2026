using System.Collections.Generic;

public static class Rules
{
    public static List<Move> GetLegalMoves(GameState state)
    {
        var pseudo = MoveGenerator.GenerateMoves(state);
        var legal = new List<Move>();

        foreach (var move in pseudo)
        {
            var next = MoveApplier.Apply(state, move);

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