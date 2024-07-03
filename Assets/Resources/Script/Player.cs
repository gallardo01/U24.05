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
        
        if (direction.magnitude > 0f)
        {
            Vector3 nextPoint = transform.position + JoystickControl.direct * Time.deltaTime * 5f;
            if(CanMove(nextPoint))
            {
                transform.position = CheckGround(nextPoint);
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            ChangeAnim("run");
            mesh.forward = JoystickControl.direct;
        }
        else
        {
            ChangeAnim("idle");
        }
    }
    private bool CanMove(Vector3 nextpoint)
    {
        RaycastHit hit;
        //Debug.Log("Ground" + Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, groundLayer));
        //Debug.Log("Stair" + Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, stairLayer));
        if (Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, stairLayer))
        {
            int stairColor = hit.collider.gameObject.GetComponent<Stair>().stairColor;
            if (colorIndex != stairColor)
            {
                // Check con` gach hay k
                if (totalBricks > 0)
                {
                    RemoveBrick();
                    // Tha gach
                    hit.collider.gameObject.GetComponent<Stair>().SetStairColor(colorIndex);
                }

                return false;
            }
        }
        return Physics.Raycast(nextpoint, Vector3.down, groundLayer);
    }

    private Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.3f;
        }
        return transform.position;
    }

    
}
