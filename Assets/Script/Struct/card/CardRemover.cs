

public class CardRemover
{
    static public GameState RemoveCard(GameState state)
    {
        var newState = state;
        
        //白のターンなら白の保留カードを初期化
        if (state.Turn.Equals(PieceColor.White))
        {
           newState.OnHoldWhiteCard = new Card();
        }
        else
        {
            newState.OnHoldBlackCard = new Card();
            
        }

        return newState;
    }
}
