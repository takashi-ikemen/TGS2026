using UnityEngine;

public abstract class Card
{
    public string name { get; protected set; }//名前は継承先クラスで設定
    public abstract GameState Use(GameState state);//全カードに実装必須
}
