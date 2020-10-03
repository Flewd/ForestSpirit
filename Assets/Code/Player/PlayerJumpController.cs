using System.Collections;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    [SerializeField] private Collider _collider;    
    [SerializeField] private Animator _animationController;
    
    private Rigidbody _rigidbody;
    
    private float distToGround = 0f;
    [SerializeField] private float jumpSpeed = 9;
    
    private string _ANIMATION_VAR_VELOCITY_Y = "velocityY";
    private string _ANIMATION_TRIGGER_JUMP = "jump";
    private string _ANIMATION_TRIGGER_JUMP_END = "jumpEnd";

    private bool isGoingUp = false;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        distToGround = _collider.bounds.extents.y;
    }

    bool IsGrounded()
    {
        if (isGoingUp)
        {
            return false;
        }
        
        // TODO maybe add more raycasts here witha slight offset to better cover bumpy terrain.
        // only 1 of the raycasts need to be true to return IsGrounded true;
        bool grounded = Physics.Raycast(transform.position + new Vector3(0,0.15f,0), Vector3.down, distToGround - 0.75f);
        
        Vector3 end = transform.position + ((distToGround - 0.75f) * Vector3.down);
        if (grounded)
        {
            Debug.DrawLine(transform.position + new Vector3(0,0.15f,0), end, Color.green);
        }
        else
        {
            Debug.DrawLine(transform.position + new Vector3(0,0.15f,0), end, Color.red);
        }
        
        return grounded;
    }
    
    bool IsAboutToGround()
    {
        if (isGoingUp)
        {
            return false;
        }
        
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 1.75f);
    }
    
    // Called from unity input system
    public void OnJump()
    {
        if (IsGrounded())
        {
            _animationController.SetTrigger(_ANIMATION_TRIGGER_JUMP);
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x ,jumpSpeed, _rigidbody.velocity.y);
            StartCoroutine(WaitThenSetIsGoingUp());
            StartCoroutine(PlayLandAnimationWhenAboutToGround());
        }
    }

    IEnumerator WaitThenSetIsGoingUp()
    {
        isGoingUp = true;
        yield return new WaitUntil(() => _rigidbody.velocity.y < 0);
        isGoingUp = false;
    }
    
    IEnumerator PlayLandAnimationWhenAboutToGround()
    {
        yield return new WaitUntil(IsAboutToGround);
        // TODO polish: when jumping between different terrain heights this can be inconsistent.
        // we could detect the distance from the terrain and play the animation a bit faster, so it 
        // dynamically changes a bit depending on how far the player is from the floor at this time.
        _animationController.SetTrigger(_ANIMATION_TRIGGER_JUMP_END);
    }

    private void Update()
    {
#if UNITY_EDITOR
        // in editor call this function so the Debug.DrawLine draws to the screen
        IsGrounded();
#endif   
        _animationController.SetFloat(_ANIMATION_VAR_VELOCITY_Y, _rigidbody.velocity.y); 
    }
}
