using UnityEngine;

public class GameController_Introduction : MonoBehaviour
{
    [SerializeField] private DoorController doorController;

    public void Start()
    {
        doorController.Initialize();
    }

    public void Update()
    {
        doorController.Tick();
    }
}
