using System.Collections;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField] private HPViews whiteHP;
    [SerializeField] private HPViews blackHP;

    [SerializeField] private CameraManager cameraManager;

    [SerializeField] private int maxHP = 10;

    public void Initialize()
    {
        whiteHP.UpdateView(maxHP);
        blackHP.UpdateView(maxHP);
    }

    public void UpdateHP(bool isWhite,int hp)
    {
        StartCoroutine(UpdateHpCoroutine(isWhite,hp));
    }

    private IEnumerator UpdateHpCoroutine(bool isWhite,int hp)
    {
        HPViews target = isWhite ? whiteHP : blackHP;

        yield return new WaitForSeconds(0.5f);

        cameraManager.ShowHpCamera(target.transform);

        yield return new WaitForSeconds(0.3f);

        target.UpdateView(hp);

        yield return new WaitForSeconds(0.5f);

        cameraManager.ShowMainCamera();
    }
}
