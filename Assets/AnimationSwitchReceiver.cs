using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitchReceiver : MonoBehaviour, InteractableSwitchReceiver
{
    [SerializeField] bool _onlyPlayOnce = true;
    [SerializeField] Animation _animation;

    private bool _hasPlayed;

    public bool CanReceiverBePlayedAgain()
    {
        if(_onlyPlayOnce && _hasPlayed)
        {
            return false;
        }

        return true;
    }

    public void SwitchFlipped()
    {
        if(CanReceiverBePlayedAgain())
        {
            _hasPlayed = true;
            _animation.Play();
        }
    }
}
