using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;

    public void OnSignalRaised()
    {
        signalEvent.Invoke();   // calls the event
    }

    private void OnEnable()
    {
        signal.RegisterListener(this);
    }
    
    private void OnDisable()
    {
        signal.DeregisterListener(this);
    }
}
