using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] private Image text1;
    [SerializeField] private Image text2;
    [SerializeField] private Image text3;

    private Image currentText;

    [SerializeField] private float fadeSpeed = 1f;

    private float targetAlpha;
    private bool isFading;

    public bool IsFadeComplete => !isFading;

    /*public void SetText(WorldEvent worldEvent)
    {
        
           
    }*/
    public void FadeIn()
    {
        targetAlpha = 0f;
        isFading = true;
    }

    public void FadeOut()
    {
        targetAlpha = 1f;
        isFading = true;
    }

    public void Tick()
    {
        if (!isFading)
            return;

        Color color = currentText.color;

        color.a = Mathf.MoveTowards(
            color.a,
            targetAlpha,
            fadeSpeed * Time.deltaTime);

        currentText.color = color;

        if (Mathf.Approximately(color.a, targetAlpha))
        {
            isFading = false;
        }
    }
}


