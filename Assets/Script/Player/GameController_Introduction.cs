using UnityEngine;

public class GameController_Introduction : MonoBehaviour
{
    [SerializeField] private DoorController doorController;
    [SerializeField] private FadeController fadeController;

    public void Start()
    {
        doorController.Initialize();

        //カーソルを非表示
        Cursor.visible = false;

        fadeController.FadeIn();
    }

    public void Update()
    {
        doorController.Tick();
        fadeController.Tick();
        
    }
}
