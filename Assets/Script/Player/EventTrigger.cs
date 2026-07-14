using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private EventController eventController;
    [SerializeField] private WorldEvent worldEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        eventController.Execute(worldEvent);
    }

}
