using UnityEngine;
using TMPro;

public class CardView : MonoBehaviour
{
    /*---
     Cardを表示・見た目を変更するためのスクリプト
     ---*/
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] public string cardName;

    public void Initialize(Card card)
    {
        cardName = card.name;

        //meshRenderer.material.mainTexture = data.texture;
    }
}





