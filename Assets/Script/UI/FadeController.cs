using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 1f;

    private float targetAlpha;
    private bool isFading;

    public bool IsFadeComplete => !isFading;

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

        Color color = fadeImage.color;

        color.a = Mathf.MoveTowards(
            color.a,
            targetAlpha,
            fadeSpeed * Time.deltaTime);

        fadeImage.color = color;

        if (Mathf.Approximately(color.a, targetAlpha))
        {
            isFading = false;
        }
    }
}
