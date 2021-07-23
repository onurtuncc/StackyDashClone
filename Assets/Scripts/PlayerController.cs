using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 800f;
    [SerializeField] private bool isMoving = false;
    private ScoreManager scoreManager;
    int endDashes=0;
    public bool atEndPhase,finished = false;
    private Animator animator;
   
    void Start()
    {
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            //Ending at chest
            rb.velocity = Vector3.zero;
            animator.SetTrigger("chestTrigger");
            
            scoreManager.endMultiply = 5;
            scoreManager.Multiplier();
        }
        else if (other.tag == "EndPhase")
        {
            endDashes = scoreManager.DashesUnder;
            atEndPhase = true;
        }
        else if (other.tag == "Box")
        {
            //We will reach chest, we dont finish the game if we don't have any dashes under
            finished = true;
        }
       
    }
    void Update()
    {
        if (atEndPhase)
        {
            if (scoreManager.DashesUnder == 0 && !finished)
            {
                //Ending without making it to chest
                animator.SetTrigger("endTrigger");
                rb.velocity = Vector3.zero;
                int roundedUp = (int)Mathf.Ceil(endDashes / 5f);
                float multiply = 1 + ((roundedUp-1) / 10f);
                
                scoreManager.endMultiply = multiply;
                scoreManager.Multiplier();
                finished = true;
            }
        }
        if (!isMoving)
        {
            
        
            if (InputManager.Instance.swipeDown)
            {
                rb.AddForce(Vector3.back * speed);
            }
            else if (InputManager.Instance.swipeUp)
            {
                rb.AddForce(Vector3.forward * speed);
            }
            else if (InputManager.Instance.swipeLeft)
            {
                rb.AddForce(Vector3.left * speed);
               
            }
            else if (InputManager.Instance.swipeRight)
            {
                rb.AddForce(Vector3.right * speed);
                
            }

        }
        if (rb.velocity == Vector3.zero)
        {
            Vector3 stopPos = transform.localPosition;
            //Character stopped
            stopPos.x = (int)Convert.ToInt32(stopPos.x);
            stopPos.z = (int)Convert.ToInt32(stopPos.z);
            transform.localPosition = stopPos;
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}