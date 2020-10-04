using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAttach : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().velocity += _rigidbody.velocity;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        print("ENTER");
        if(other.tag == "Player")
        {
            other.transform.parent = transform;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        print("EXIT");
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
*/
}
