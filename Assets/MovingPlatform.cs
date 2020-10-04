using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, InteractableSwitchReceiver
{
    [SerializeField] private Rigidbody _platformRigidBody;
    [SerializeField] private Transform _targetPosition;

    [SerializeField] private bool _loop = true;
    [SerializeField] private float _duration = 1;
    [SerializeField] private Ease _easeType = Ease.Linear;

    [SerializeField] private bool _controlledByButton = false;

    [Header("Only applies if controlled by button")]
    [SerializeField] private bool _startInMotion = false;

    void Start()
    {
        StartMotion();

        if (_controlledByButton && _startInMotion == false)
        {
            _platformRigidBody.DOTogglePause();
            return;
        }
    }

    private void StartMotion()
    {
        int loopCount = (_loop ? -1 : 0);

        TweenParams tweenParams = new TweenParams()
            .SetLoops(loopCount, LoopType.Yoyo)
            .SetEase(_easeType);

        _platformRigidBody.DOMove(_targetPosition.position, _duration).SetAs(tweenParams);
    }

    void InteractableSwitchReceiver.SwitchFlipped()
    {
        _platformRigidBody.DOTogglePause();
    }
}
