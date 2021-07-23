using UnityEngine;

public class StackScript : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    private ScoreManager scoreManager;
    private StackManager stackManager;

    private void Awake()
    {
        
        //The highest in hierarchy is player
        player = this.transform.root.gameObject;
        //Setting road parent to drop dashes there

        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        stackManager = player.GetComponent<StackManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //Trigger enter with dash   
        if (other.tag == "Dash")
        {
            other.tag = "Untagged";
            stackManager.StackDash(other.gameObject);
            //New bottom dash get's the script
            other.gameObject.AddComponent<StackScript>();
            //destroying current script
            Destroy(this);
        }
        else if (other.tag == "Road")
        {
            //Stopping on road when no dashes under
            if (scoreManager.DashesUnder ==0)
            {
                //Sending player back if he doesnt have enough dashes
                
                Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
                playerVelocity *= 1.5f;
                player.GetComponent<Rigidbody>().velocity -= playerVelocity;
                


            }
            //If we have dashes go on
            else
            {
                other.tag = "Untagged";
                stackManager.DropDash(other.gameObject,0);
                Destroy(this);
            }

        }
        else if (other.tag == "HeightRoad")
        {
            other.tag = "Untagged";
            stackManager.DropDash(other.gameObject, 1);
            Destroy(this);
        }
       
    }
    



}

