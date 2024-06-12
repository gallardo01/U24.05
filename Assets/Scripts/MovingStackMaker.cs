using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class MovingStackMaker : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask Grounded;

    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    public bool canUp = false;
    public bool canDown = false;
    public bool canLeft = false;
    public bool canRight = false;

    public float speed;

    public float hight = 0.5f;
    public float StackCount = 0;
    public float BrickObject = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        canUp = CheckIsMoveStep(up);
        canDown = CheckIsMoveStep(down);
        canLeft = CheckIsMoveStep(left);
        canRight = CheckIsMoveStep(right);


        Moving();
    }

    private bool CheckIsMoveStep(Transform DirectionPoint) 
    {
        Debug.DrawLine(DirectionPoint.position, DirectionPoint.position + Vector3.down * 1.1f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(DirectionPoint.position, Vector3.down, out hit, 1.1f, Grounded))
        {
            return true;
        }
        else
        {
            return false;
        }

        //return Physics.Raycast(DirectionPoint.position, Vector3.down, 1f, Grounded);
    }

    private void Moving() 
    {
        if (UnityEngine.Input.GetKey(KeyCode.W))
        {
            if (canUp) 
            {
                transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
            }
        }
        else if (UnityEngine.Input.GetKey(KeyCode.S)) 
        {
            if (canDown) 
            {
                transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
            }
        }else if (UnityEngine.Input.GetKey(KeyCode.D)) 
        {
            if (canRight) 
            {
                transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
            }
        }else if (UnityEngine.Input.GetKey(KeyCode.A)) 
        {
            if (canLeft) 
            {
                transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Stackable"))
    //    {
    //        Vector3 newPost = transform.position;
    //        newPost.y += hight;
    //        transform.position = newPost;

    //        //push brich 
    //        Transform t = other.transform;
    //        t.tag = "Untagged";
    //        t.SetParent(this.transform);
    //        t.localPosition = new Vector3(0, StackCount * BrickObject, 0);

    //        StackCount++;
    //        Debug.Log("Push Stack: " + StackCount);
    //    }
    //}



}
