using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text turnText;

    [SerializeField]
    private CrystalHPView whiteHP;

    [SerializeField]
    private CrystalHPView blackHP;

    public void Initialize()
    {
        //whiteHP.Initialize(6);
        //blackHP.Initialize(6);
    }

    public void UpdateHP(int white, int black)
    {
        //whiteHP.UpdateHP(white);
        //blackHP.UpdateHP(black);
    }

    public void UpdateTurn(PieceColor turn)
    {
        turnText.text = turn.ToString();
    }
}