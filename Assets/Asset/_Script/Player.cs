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
                transform.position = nextPoint;
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
        Debug.DrawLine(nextpoint, nextpoint + Vector3.down * 2f, Color.red, 2f);
        return Physics.Raycast(nextpoint, Vector3.down,out hit, 2f, groundLayer);
    }
}
