using System.Collections.Generic;

public static class MoveGenerator
{
    public static List<Move> GenerateMoves(GameState state)
    {
        var moves = new List<Move>();

        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 7; y++){
                var piece = state.Board.Get(x, y);
                //空きマス、相手の駒、バインドの指定がある場合は無視
                if (piece.IsEmpty || piece.Color != state.Turn || (x == state.BindX && y == state.BindY)) continue;

                //駒の種類を判定
                switch (piece.Type)
                {
                    case PieceType.Bishop:
                        GenerateBishop(state, x, y, moves);
                        break;
                    case PieceType.Rook:
                        GenerateRook(state, x, y, moves);
                        break;
                    case PieceType.Knight:
                        GenerateKnight(state, x, y, moves);
                        break;
                    case PieceType.King:
                        GenerateKing(state, x, y, moves);
                        break;
                    case PieceType.Pawn:
                        GeneratePawn(state, x, y, moves);
                        break;
                }
            }
        return moves;
    }

    //ポーン（RestrictMoveCard用のPieceType）
    static void GeneratePawn(GameState state, int x, int y, List<Move> moves)
    {
        int nx = x + state.RestrictX;
        int ny = y + state.RestrictY;

        moves.Add(new Move(x, y, nx, ny));
    }

    //ビショップ
    static void GenerateBishop(GameState state, int x, int y, List<Move> moves)
    {
        //移動可能マス
        int[] dx = { 1, 1, -1, -1 };
        int[] dy = { 1, -1, 1, -1 };

        var piece = state.Board.Get(x, y);

        for (int d = 0; d < 4; d++)
        {
            int nx = x;
            int ny = y;


            while (true)
            {
                //どこまで行けるか１マスずつ見る
                nx += dx[d];
                ny += dy[d];

                //ボード外なら中断
                if (!state.Board.IsInside(nx, ny)) break;

                var target = state.Board.Get(nx, ny);

                if (target.IsEmpty)
                {
                    //空きマスならmoveに追加
                    moves.Add(new Move(x, y, nx, ny));
                }
                else
                {
                    //コマがあったら中断
                    break;
                }
            }
        }
    }

        //ルーク
        static void GenerateRook(GameState state, int x, int y, List<Move> moves)
    {
                   //上 下 右 左
        int[] dx = { 0, 0, 1,  -1};
        int[] dy = { 1, -1, 0, 0 };

        var piece = state.Board.Get(x, y);

        for (int d = 0; d < 4; d++)
        {
            int nx = x;
            int ny = y;

            
            while (true)
            {
                //どこまで行けるか１マスずつ見る
                nx += dx[d];
                ny += dy[d];

                //ボード外なら中断
                if (!state.Board.IsInside(nx, ny)) break;

                var target = state.Board.Get(nx, ny);

                if (target.IsEmpty)
                {
                    //空きマスならmoveに追加
                    moves.Add(new Move(x, y, nx, ny));
                }
                else
                {
                    //コマがあったら中断
                    break;
                }
            }
        }
    }

    //ナイト
    static void GenerateKnight(GameState state, int x, int y, List<Move> moves)
    {
        //移動可能マス
        int[] dx = { 2, -2, 2, -2, 1, -1, 1, -1 };
        int[] dy = { 1, -1, -1, 1, 2, -2, -2, 2 };

        var piece = state.Board.Get(x, y);

        int nx = x;
        int ny = y;

        //移動可能マスに移動できるか確認
        for(int d = 0; d < 8; d++)
        {
            nx = x + dx[d];
            ny = y + dy[d];
            if (state.Board.IsInside(nx, ny) && state.Board.Get(nx, ny).IsEmpty)
            {
                moves.Add(new Move(x, y, nx, ny));
            }
        }
    }

    //キング
    static void GenerateKing(GameState state, int x, int y, List<Move> moves)
    {
        //移動可能マス
        int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
        int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

        var piece = state.Board.Get(x, y);

        int nx = x;
        int ny = y;

        //移動可能マスに移動できるか確認
        for (int d = 0; d < 8; d++)
        {
            nx = x + dx[d];
            ny = y + dy[d];
            if (state.Board.IsInside(nx, ny) && state.Board.Get(nx, ny).IsEmpty)
            {
                moves.Add(new Move(x, y, nx, ny));
            }
        }
    }
}