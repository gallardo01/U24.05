using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;   
        
        if(direction.magnitude > 0f)
        {
            //transform.Translate(direction * speed * Time.deltaTime);
            Vector3 nextPoint = transform.position + JoystickControl.direct * Time.deltaTime * speed;
            
            if (CanMove(nextPoint))
            {
                transform.position = nextPoint; 
            }
            ChangeAnim("run");
            mesh.forward = JoystickControl.direct;
        }
        else
        {
            ChangeAnim("idle");  
        }
    }

    //private bool CanMove(Vector3 nextPoint)
    //{
    //    RaycastHit hit;
    //    //Debug.DrawRay(nextPoint, Vector3.down, Color.red, 0.01f);
    //    //Debug.Log("Ground: " + Physics.Raycast(nextPoint, Vector3.down, out hit, 9f, groundLayer));
    //    //Debug.Log("Stair: " + Physics.Raycast(nextPoint, Vector3.down, out hit, 9f, stairLayer));

    //    if(Physics.Raycast(nextPoint, Vector3.down, out hit, 9f, stairLayer))
    //    {
    //        int stairColor = hit.collider.gameObject.GetComponent<Stair>().stairColor;
    //        if (colorIndex != stairColor)
    //        {
    //            //Check con gach hay khong
    //            if(totalBrick > 0)
    //            {
    //                RemoveBrick();
    //                //Tha gach
    //                hit.collider.gameObject.GetComponent<Stair>().SetStairColor(colorIndex);    
    //            }
    //            return false;
    //        }
    //    }
    //    return Physics.Raycast(nextPoint, Vector3.down, groundLayer);
    //}
}
