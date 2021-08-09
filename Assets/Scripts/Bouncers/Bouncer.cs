using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer:MonoBehaviour
{
    public int findHitType(Vector3 velocityOfPlayer)
    {
        //Finding hit type
        //If hit type:0 player comes from horizontal plane
        //If hit type:1 player comes from vertical plane
        int hitType;
        float zValue = velocityOfPlayer.z;
        if (zValue != 0)
        {
            hitType = 1;
        }
        else
        {
            hitType = 0;
        }
        return hitType;
    }

    public int OnTriggerPhase(Collider other)
    {
        //Getting players velocity to check where he comes from
        Vector3 velocityOfPlayer = other.transform.root.GetComponent<Rigidbody>().velocity;
        int hitType = findHitType(velocityOfPlayer);
        return hitType;
        
    }


}
