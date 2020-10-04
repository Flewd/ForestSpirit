using UnityEngine;

public class NpcLookAt : MonoBehaviour
{

    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(
            playerTransform.position.x, 
            transform.position.y, 
            playerTransform.position.z));
    }
}
