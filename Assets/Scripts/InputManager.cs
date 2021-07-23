using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Facilitate access from other scripts 
    public static InputManager Instance { get; set; }
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    public Vector2 swipeDelta, startTouch;
    //Minimum length of swipe to count it as a swipe
    private const float deadZone = 50;
    
    private void Awake()
    {
       
        Instance = this;
    }

    private void Update()
    {
        //Resetting all booleans
        tap = false;
        swipeLeft = false;
        swipeRight = false;
        swipeDown = false;
        swipeUp = false;

        //Checking if there is a touch
        #region PC Controls
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        #endregion
        #region Mobile Controls
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = Vector2.zero;
                swipeDelta = Vector2.zero;
            }

        }
        #endregion

        //Getting swipe vector
        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            // for Mobile
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            // for PC
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;

            }


        }

        //Checking if swipe length is greater than dead zone
        if (swipeDelta.magnitude > deadZone)
        {
            //We pass the dead zone
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if (x < 0)
                {
                    //Left
                    swipeLeft = true;
                }
                else
                {
                    //Right
                    swipeRight = true;
                }
            }
            else
            {
                //Up or Down
                if (y < 0)
                {
                    // Down
                    swipeDown = true;
                }
                else
                {
                    // Up
                    swipeUp = true;
                }
            }
            //Resetting touch
            startTouch = Vector2.zero;
            swipeDelta = Vector2.zero;
        }
    }
}
