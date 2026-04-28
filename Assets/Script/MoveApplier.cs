public static class MoveApplier
{
    public static GameState Apply(GameState state, Move move)
    {
        var newState = state;

        var piece = newState.Board.Get(move.FromX, move.FromY);

        //移動先が地雷なら爆発
        if(move.ToX == state.Mine.GetMineX() && move.ToY == state.Mine.GetMineY())
        {
            newState.Board.Set(move.ToX, move.ToY, Piece.Empty);
            //地雷再設置　0 <= x < 5, 0 <= y < 7
            newState.Mine = MineGenerator.GenerateMine(0,5,0,7,newState.Board);
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