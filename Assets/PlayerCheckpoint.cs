using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public static Vector3 LastRegisteredCheckpointPosition = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LastRegisteredCheckpointPosition = this.transform.position;
        }
    }
}
