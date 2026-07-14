using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_Introduction : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        //暗くする
        fadeController.FadeOut();

        while (!fadeController.IsFadeComplete)
            yield return null;

        //非同期ロード開始
        AsyncOperation operation =
            SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
            yield return null;

        //読み込み完了後に切り替え
        operation.allowSceneActivation = true;
    }
}
