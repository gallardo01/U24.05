using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;
        if (direction != Vector3.zero)
        {
            body.rotation = Quaternion.LookRotation(direction);
            body.Translate(direction * speed * Time.deltaTime, Space.World);
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        }
        
    }

   
}
