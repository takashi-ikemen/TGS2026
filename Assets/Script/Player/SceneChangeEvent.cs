using UnityEngine;

public class SceneChangeEvent : WorldEvent
{
    [SerializeField] private SceneLoader_Introduction sceneLoader;
    [SerializeField] private string loadScene;

    public override void Execute()
    {
        sceneLoader.LoadScene(loadScene);
    }
}
