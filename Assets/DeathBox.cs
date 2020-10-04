using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerDeathHandler deathHandler = other.GetComponentInChildren<PlayerDeathHandler>();

            if(deathHandler == null)
            {
                Debug.LogError("Player is missing PlayerDeathHandlerComponent");
            }
            else
            {
                deathHandler.TransitionPlayerBackToCheckpoint();
            }
        }
    }
}
