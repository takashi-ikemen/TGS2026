using UnityEngine;

public class DoorEvent : WorldEvent
{
    [SerializeField] private DoorController doorController;

    public  override void Execute()
    {
        doorController.Open();
    }
}
