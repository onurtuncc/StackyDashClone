using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport:MonoBehaviour
{
    public Vector3 myPoint;
    public Teleport outTeleport;
    public bool isOpen;

    private void Awake()
    {
       
        isOpen = true;
        myPoint = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider" && isOpen)
        {
            
            if (other.attachedRigidbody.velocity.magnitude != 0)
            {
                TeleportPlayer(other.transform.root);
            }
            
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCollider" )
        {
            isOpen = true;
            
        }
    }

    public void TeleportPlayer(Transform player)
    {
        outTeleport.isOpen = false;
        player.localPosition = new Vector3(outTeleport.transform.position.x,player.localPosition.y,outTeleport.transform.position.z);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    

}
