using System;

public class RandomHPCard : Card
{
    public RandomHPCard()
    {
        name = "RandomHPCard";
    }

    public override GameState Use(GameState state)
    {
        Random random = new Random();
        int upDown = random.Next(0, 2);//回復かダメージかをランダムに決定、0なら回復、1ならダメージ

        if (state.Turn.Equals(PieceColor.White))
        {
            if(upDown == 0)
            {
                state.WhiteHP += 1;
            }
            else
            {
                state.WhiteHP -= 1;
            }
        }
        else//黒が使ったとき
        {
            if(upDown == 0)
            {
                state.BlackHP += 1;
            }
            else
            {
                state.BlackHP -= 1;
            }
        }

        return state;
    }
}
