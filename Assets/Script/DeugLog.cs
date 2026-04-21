using JetBrains.Annotations;
using UnityEngine;

public class DeugLog : MonoBehaviour
{
    GameState state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //Board board = new Board();
        state = GameInitializer.CreateInitial();
        Piece piece = state.Board.Get(2,0);
        Debug.Log(piece.Type);
        Debug.Log(piece.Color);
    }

 
}
