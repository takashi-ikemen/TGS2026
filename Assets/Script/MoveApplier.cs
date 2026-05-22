using System;

public static class MoveApplier
{
    public static GameState Apply(GameState state, Move move)
    {
        var newState = state;

        newState.MineExploded = false;

        var piece = newState.Board.Get(move.FromX, move.FromY);

        //ルークの移動経路に地雷があれば爆発
        if (piece.Type.Equals(PieceType.Rook))
        {
            int dx = Math.Sign(move.ToX - move.FromX); // -1, 0, 1
            int dy = Math.Sign(move.ToY - move.FromY); // -1, 0, 1

            int x = move.FromX + dx;
            int y = move.FromY + dy;

            newState.Board.Set(move.ToX, move.ToY, piece);

            while (x != move.ToX || y != move.ToY)
            {
                // 地雷チェック
                if (x == state.Mine.GetMineX() && y == state.Mine.GetMineY())
                {
                    //地雷爆発処理
                    newState.MineExploded = true;
                    newState.ExplosionX = state.Mine.GetMineX();
                    newState.ExplosionY = state.Mine.GetMineY();
                    newState.Board.Set(move.ToX, move.ToY, Piece.Empty);
                    //地雷再設置　0 <= x < 5, 0 <= y < 7
                    newState.Mine = MineGenerator.GenerateMine(0, 5, 0, 7, newState.Board);
                }

                x += dx;
                y += dy;
            }

        }

        //その他、移動先が地雷なら爆発
        else if (move.ToX == state.Mine.GetMineX() && move.ToY == state.Mine.GetMineY())
        {
            //地雷爆発処理
            newState.MineExploded = true;
            newState.ExplosionX = state.Mine.GetMineX();
            newState.ExplosionY = state.Mine.GetMineY();

            newState.Board.Set(move.ToX, move.ToY, Piece.Empty);
            //地雷再設置　0 <= x < 5, 0 <= y < 7
            newState.Mine = MineGenerator.GenerateMine(0, 5, 0, 7, newState.Board);
        }
        //違えば移動
        else
        {
            newState.Board.Set(move.ToX, move.ToY, piece);
        }
        newState.Board.Set(move.FromX, move.FromY, Piece.Empty);

        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        return newState;
    }
}