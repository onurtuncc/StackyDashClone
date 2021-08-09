using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRightBouncer : Bouncer,IBounce
{ 
    private float speed = 800f;
    private int hitType = 0;


    public void bouncePlayer(Rigidbody player)
    {
        player.velocity = Vector3.zero;
        if (hitType == 0)
        {
            //Bounce player back if player comes from horizontal plane
            player.AddForce(Vector3.back * speed);
        }
        else
        {
            //Bounce player left if player comes from vertical plane
            player.AddForce(Vector3.left * speed);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        //Using common methods from superclass
        if (other.tag == "PlayerCollider")
        {
            hitType = base.OnTriggerPhase(other);
            bouncePlayer(other.transform.root.GetComponent<Rigidbody>());


        }

    }
}
