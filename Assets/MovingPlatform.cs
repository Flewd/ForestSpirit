using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody _platformRigidBody;
    [SerializeField] private Transform _targetPosition;

    [SerializeField] private bool _loop = true;
    [SerializeField] private float _duration = 1;
    [SerializeField] private Ease _easeType = Ease.Linear;

    // Start is called before the first frame update
    void Start()
    {
        int loopCount = (_loop ? -1 : 0);

        TweenParams tweenParams = new TweenParams()
            .SetLoops(loopCount, LoopType.Yoyo)
            .SetEase(_easeType);

        _platformRigidBody.DOMove(_targetPosition.position, _duration).SetAs(tweenParams);
    }
}
