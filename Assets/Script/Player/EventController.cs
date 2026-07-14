using UnityEngine;
using UnityEngine.LightTransport;

public class EventController : MonoBehaviour
{
    [SerializeField] private WorldEvent[] worldEvents;

    public void Initialize()
    {
        foreach(WorldEvent worldEvent in worldEvents)
        {
            if(worldEvent != null)
            {
                worldEvent.Initialize(this);
            }
        }
    }

    public void Execute(WorldEvent worldEvent)
    {
        if (worldEvent == null) return;

        Debug.Log(worldEvent);
       
        worldEvent.Execute();
    }
}
