using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform mesh;
    // Start is called before the first frame update
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
            transform.Translate(direction * Time.deltaTime * 5f);
            mesh.forward = JoystickControl.direct;
        }
    }
}
