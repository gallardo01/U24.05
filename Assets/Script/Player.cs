using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    [SerializeField] LayerMask brickLayer;

    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;

    void Start()
    {
        UpdateMoveStatus();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(canMoveUp)
            {
                transform.position += new Vector3(0, 0, 1);
                UpdateMoveStatus();
            }    
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (canMoveDown)
            {
                transform.position += new Vector3(0, 0, -1);
                UpdateMoveStatus();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (canMoveLeft)
            {
                transform.position += new Vector3(-1, 0, 0);
                UpdateMoveStatus();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (canMoveRight)
            {
                transform.position += new Vector3(1, 0, 0);
                UpdateMoveStatus();
            }
        }
    }

    public bool CanMoveTo(Transform target)
    {
        return Physics.Raycast(target.position, Vector3.down, 10f, brickLayer);
    }    

    public void UpdateMoveStatus()
    {
        canMoveUp = CanMoveTo(up);
        canMoveDown = CanMoveTo(down);
        canMoveLeft = CanMoveTo(left);
        canMoveRight = CanMoveTo(right);
    }
}
