using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] private Rigidbody rb;

    //private float jumpHight = 0.5f;
    private float stackCount = 0;
    private float objectHight = 0.3f;

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

            // xác định hướng vuốt của chuột
            StartCoroutine(DetectSwipe(startMousePosition));
        }

    }

    private IEnumerator DetectSwipe(Vector3 startMousePosition)
    {
        
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }

        Vector3 endMousePosition = Input.mousePosition;
        Vector3 swipeDirection = endMousePosition - startMousePosition;
        float minSwipeDistance = 50f; // khoảng cách vuốt tối thiểu

        if (swipeDirection.magnitude >= minSwipeDistance)
        {
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x > 0)
                {
                    MovePlayer(Direct.Right);
                }
                else
                {
                    MovePlayer(Direct.Left);
                }
            }
            else
            {
                if (swipeDirection.y > 0)
                {
                    MovePlayer(Direct.Forward);
                }
                else
                {
                    MovePlayer(Direct.Back);
                }
            }
        }
    }

    private void MovePlayer(Direct direction) 
    {
        if (direction == Direct.Forward) 
        {
            rb.AddForce(new Vector3(400, 0, 0));
        }
        else if (direction == Direct.Back) 
        {
            rb.AddForce(new Vector3(-400, 0, 0)); 
        }
        else if (direction == Direct.Right) 
        {
            rb.AddForce(new Vector3(0, 0, -400)); 
        }
        else if (direction == Direct.Left) 
        {
            rb.AddForce(new Vector3(0, 0, 400)); 
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Stackable"))
    //    {
    //        //nhảy nhân vật lên
    //        Vector3 newPos = Player.position;
    //        newPos.y += objectHight;
    //        Player.position = newPos;

    //        //xếp đối tượng rưới chân nhân vật
    //        Transform t = other.transform;
    //        t.tag = "Untagged";
    //        t.SetParent(this.transform);
    //        t.localPosition = new Vector3(0, stackCount * objectHight, 0);

    //        stackCount++;
    //        Debug.Log("Brick " + stackCount);
    //    }
    //}

}
