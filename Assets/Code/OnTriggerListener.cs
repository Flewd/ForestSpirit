using UnityEngine;

public interface IOnTriggerListener
{
    void OnTriggerEnter(Collider other);
    void OnTriggerStay(Collider other);
    void OnTriggerExit(Collider other);
}

public class OnTriggerListener : MonoBehaviour
{
    [SerializeReference] private Component[] _triggerListeners;
    private IOnTriggerListener[] _listeners;
    
    private void Awake()
    {
        _listeners = new IOnTriggerListener[_triggerListeners.Length];

        for (int i = 0; i < _triggerListeners.Length; i++)
        {
            _listeners[i] = _triggerListeners[i] as IOnTriggerListener;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (IOnTriggerListener listener in _listeners)
        {
            listener.OnTriggerEnter(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (IOnTriggerListener listener in _listeners)
        {
            listener.OnTriggerStay(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (IOnTriggerListener listener in _listeners)
        {
            listener.OnTriggerExit(other);
        }
    }
}
