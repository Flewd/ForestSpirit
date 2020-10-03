using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private Animator _animationController;
    
    [SerializeField] private float _moveSpeed = 10;
    
    private Vector2 _currentMovementInput = Vector2.zero;
    
    private Transform _camTransform;
    private Vector3 _playerDirection;

    private string _ANIMATION_VAR_VELOCITY = "velocity";
    private const float diagonalMoveLimiter = 0.70f;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camTransform = Camera.main.transform;
    }

    public void OnMove(InputValue input)
    {
        _currentMovementInput = input.Get<Vector2>();
    }
    
    private void FixedUpdate()
    {
        if (_currentMovementInput != Vector2.zero)
        {
            _playerDirection = new Vector3(_currentMovementInput.x, 0, _currentMovementInput.y);
            
            if (_currentMovementInput.x != 0 && _currentMovementInput.y != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                _playerDirection.x *= diagonalMoveLimiter;
                _playerDirection.z *= diagonalMoveLimiter;
            }

            Vector3 camRelativeDirection = _camTransform.TransformDirection(new Vector3(_playerDirection.x, _playerDirection.y, _playerDirection.z));
            camRelativeDirection.y = 0;

            _rigidbody.velocity = new Vector3(
                (_moveSpeed) * camRelativeDirection.x, 
                _rigidbody.velocity.y,  
                (_moveSpeed) * camRelativeDirection.z);

            HandleRotation();
            _animationController.SetFloat(_ANIMATION_VAR_VELOCITY, Mathf.Clamp(_rigidbody.velocity.magnitude, 0.4f, _moveSpeed));
        }
        else
        {
            _rigidbody.velocity = new Vector3(0,_rigidbody.velocity.y,0);
            _animationController.SetFloat(_ANIMATION_VAR_VELOCITY, 0.4f);    // 0.5 is the cutoff in the controller when the animations switch
        }
    }

    private void HandleRotation()
    {
        float targetRotation = Mathf.Atan2(_playerDirection.x, _playerDirection.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
        Quaternion lookAt = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(0,targetRotation,0),
            0.3f);
        _rigidbody.rotation = lookAt;
    }
}
