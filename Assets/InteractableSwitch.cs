using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractableSwitchReceiver
{
    void SwitchFlipped();
}

public class InteractableSwitch : MonoBehaviour
{
    [SerializeField] GameObject _buttonPromptIndicator;
    [SerializeField] private GameObject _targetSwitchReceiver;

    private InteractableSwitchReceiver receiver;

    void Start()
    {
        _buttonPromptIndicator.SetActive(false);
        receiver = _targetSwitchReceiver.GetComponentWithInterface<InteractableSwitchReceiver>();

        if (receiver == null)
        {
            Debug.LogError($"receiver does not implement InteractableSwitchReceiver in {gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _buttonPromptIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _buttonPromptIndicator.SetActive(false);
        }
    }

    public void SwitchPressed()
    {
        receiver.SwitchFlipped();
    }

    // Called from unity input system
    public void OnInteract()
    {
        if (_buttonPromptIndicator.activeSelf)
        {
            SwitchPressed();
        }
    }
}
