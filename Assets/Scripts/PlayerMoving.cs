using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    public enum Direct { None, Forward, Back, Right, Left }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 startMousePosition = Input.mousePosition;

            // Listen for mouse button release to determine swipe direction
            StartCoroutine(DetectSwipe(startMousePosition));
        }

    }

    private IEnumerator DetectSwipe(Vector3 startMousePosition)
    {
        // Wait until the mouse button is released
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }

        Vector3 endMousePosition = Input.mousePosition;
        Vector3 swipeDirection = endMousePosition - startMousePosition;
        float minSwipeDistance = 50f; // Minimum swipe distance to trigger an action

        if (swipeDirection.magnitude >= minSwipeDistance)
        {
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x > 0)
                {
                    // Swipe right
                    MovePlayer(Direct.Right);
                }
                else
                {
                    // Swipe left
                    MovePlayer(Direct.Left);
                }
            }
            else
            {
                if (swipeDirection.y > 0)
                {
                    // Swipe up
                    MovePlayer(Direct.Forward);
                }
                else
                {
                    // Swipe down
                    MovePlayer(Direct.Back);
                }
            }
        }
    }

    private void MovePlayer(Direct direction) 
    {
        if (direction == Direct.Forward) 
        {
            rb.AddForce(new Vector3(400, 0, 0)); // len theo truc x
        }
        else if (direction == Direct.Back) 
        {
            rb.AddForce(new Vector3(-400, 0, 0)); //xuong theo truc x
        }
        else if (direction == Direct.Right) 
        {
            rb.AddForce(new Vector3(0, 0, -400)); //trai theo truc z
        }
        else if (direction == Direct.Left) 
        {
            rb.AddForce(new Vector3(0, 0, 400)); // phai theo z
        }
    }
    
}
