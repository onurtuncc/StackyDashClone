using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{

    private ScoreManager scoreManager;
    [SerializeField]Transform dashesParent;
    [SerializeField]GameObject prevDash;
    [SerializeField]private float dashHeight = 0.045f;
    private PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();


        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        dashesParent = transform.GetChild(2);
        prevDash = dashesParent.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StackDash(GameObject dash)
    {
        //Setting score and dashes under
        scoreManager.Score += 1;
        scoreManager.DashesUnder += 1;
        //Getting dash under Dash Parent
        dash.transform.SetParent(dashesParent);
        Vector3 pos = prevDash.transform.localPosition;
        pos.y -= dashHeight;
        dash.transform.localPosition = pos;
        //Player gets higher
        Vector3 playerPos = transform.localPosition;
        playerPos.y += dashHeight;
        transform.localPosition = playerPos;

        prevDash = dash;
        prevDash.GetComponent<BoxCollider>().isTrigger = false;

    }

    public void DropDash(GameObject dash, int type)
    {
        dash.GetComponent<BoxCollider>().isTrigger = false;
        //One dash dropped
        scoreManager.DashesUnder -= 1;
        //getting our index to find next dash to have the script
        int i = prevDash.transform.GetSiblingIndex();
        prevDash.transform.SetParent(dash.transform.parent);
        Vector3 pos = prevDash.transform.localPosition;
        prevDash.transform.localPosition = dash.transform.localPosition;
        Vector3 playerPos = transform.localPosition;
        if (type == 0)
        {
            //Normal road
            playerPos.y -= dashHeight;
        }
        else
        {
            //Ending height roads
            playerPos.y += dashHeight;
        }
        if (playerController.atEndPhase)
        {
            scoreManager.Score += 1;
        }

        transform.localPosition = playerPos;
        if (dashesParent.childCount != 0)
        {
            prevDash = dashesParent.GetChild(i - 1).gameObject;
            prevDash.AddComponent<StackScript>();

        }
        
        
    }
}
