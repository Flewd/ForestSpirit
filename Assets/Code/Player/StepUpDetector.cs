using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepUpDetector : MonoBehaviour
{
    [SerializeField] private TriggerDetector _triggerDetector;
    [SerializeField] private Transform _raycastTransform;
    
    public bool CanStepUp()
    {
        if (_triggerDetector.IsTriggerActivated)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(_raycastTransform.position, _raycastTransform.TransformDirection(Vector3.forward), out hit, 0.5f))
            {
                Debug.DrawRay(_raycastTransform.position, _raycastTransform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                return false;
            }
            else
            {
                Debug.DrawRay(_raycastTransform.position, _raycastTransform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
                return true;
            }
        }

        return false;
    }
}
