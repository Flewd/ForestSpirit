using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    private const int NUMBER_OF_UPDATES_TILL_NOT_COLLIDING = 3; 
    private int notCollidingCount = 0;
    
    public bool IsTriggerActivated = false;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        notCollidingCount -= 1;
        
        if (notCollidingCount <= 0)
        {
            IsTriggerActivated = false;
        }
        else
        {
            IsTriggerActivated = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        notCollidingCount = NUMBER_OF_UPDATES_TILL_NOT_COLLIDING;
    }
}
