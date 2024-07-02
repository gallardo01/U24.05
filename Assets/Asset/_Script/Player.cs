using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField] Transform body;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask stairLayer;



    void Update()
    {
        Vector3 direction = JoystickControl.direct.normalized;
     
        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            body.rotation = newRotation;
            Vector3 nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if (CanMove(nextPoint))
            {
                Debug.DrawRay(nextPoint, Vector3.down, Color.red);
                transform.position = CheckGround(nextPoint);
            }
            ChangeAnim("run");
        } else
        {
            ChangeAnim("idle");
        }
    }
    
    private bool CanMove(Vector3 nextpoint)
    {
        RaycastHit hit;
       
        if (Physics.Raycast(nextpoint,Vector3.down,out hit, 2f, stairLayer))
        {
            //Debug.Log("bantrung");
            int stepColor = hit.collider.GetComponent<Bridge>().stepFloorColor;
            if (stepColor != colorIndex)
            {
                Debug.Log("khong di duoc");
                return false;
            } else
            {
                Debug.Log("di duoc");
                return true;
            }
        }
        Debug.Log("di duoc");
        return Physics.Raycast(nextpoint, Vector3.down, groundLayer);
    }
    private Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint,Vector3.down,out hit, 2f,groundLayer))
        {
            return hit.point + Vector3.up * 0.3f;
        }
        return transform.position;
    }
}
