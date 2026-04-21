using System.Collections.Generic;

public static class MoveGenerator
{
    public static List<Move> GenerateMoves(GameState state)
    {
        var moves = new List<Move>();

        for (int x = 0; x < 8; x++)
        for (int y = 0; y < 8; y++)
        {
            var piece = state.Board.Get(x, y);
            if (piece.IsEmpty || piece.Color != state.Turn) continue;

            switch (piece.Type)
            {
                case PieceType.Pawn:
                    GeneratePawn(state, x, y, moves);
                    break;

                case PieceType.Rook:
                    GenerateRook(state, x, y, moves);
                    break;
            }
        }

        return moves;
    }

    static void GeneratePawn(GameState state, int x, int y, List<Move> moves)
    {
        var piece = state.Board.Get(x, y);
        int dir = piece.Color == PieceColor.White ? 1 : -1;

        int ny = y + dir;

        if (state.Board.IsInside(x, ny) && state.Board.Get(x, ny).IsEmpty)
        {
            moves.Add(new Move(x, y, x, ny));
        }
    }

    static void GenerateRook(GameState state, int x, int y, List<Move> moves)
    {
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        var piece = state.Board.Get(x, y);

        for (int d = 0; d < 4; d++)
        {
            int nx = x;
            int ny = y;

            while (true)
            {
                nx += dx[d];
                ny += dy[d];

                if (!state.Board.IsInside(nx, ny)) break;

                var target = state.Board.Get(nx, ny);

                if (target.IsEmpty)
                {
                    moves.Add(new Move(x, y, nx, ny));
                }
                else
                {
                    if (target.Color != piece.Color)
                        moves.Add(new Move(x, y, nx, ny));
                    break;
                }
            }
        }
    }
}