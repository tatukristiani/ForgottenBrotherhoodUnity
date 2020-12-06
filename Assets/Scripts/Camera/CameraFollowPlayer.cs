using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*THIS CLASS ISN'T USED ANYWHERE, MADE THIS FOR FUTURE DEVELOPMENT*/
public class CameraFollowPlayer : MonoBehaviour
{

    public Transform player;
    private float smoothness = 0.1f;
    private Vector3 camerasOffset; 


    void Start()
    {
        camerasOffset.z = -1;
        
    }
  

    void FixedUpdate()
    {
        if(player != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position + camerasOffset, smoothness);
            transform.position = newPosition;

        }  
    }
}
