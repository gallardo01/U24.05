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

    public enum MoveState
    {
        Up,
        Down,
        Left,
        Right
    }

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(CanMoveTo(MoveState.Up))
            {
                MovePlayer(MoveState.Up);
            }    
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CanMoveTo(MoveState.Down))
            {
                MovePlayer(MoveState.Down);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CanMoveTo(MoveState.Left))
            {
                MovePlayer(MoveState.Left);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CanMoveTo(MoveState.Right))
            {
                MovePlayer(MoveState.Right);
            }
        }
    }

    public bool CanMoveTo(MoveState state)
    {
        if(state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, 10f, brickLayer);
        }
        else if(state == MoveState.Down)
        {
            return Physics.Raycast(down.position, Vector3.down, 10f, brickLayer);
        }
        else if(state == MoveState.Left)
        {
            return Physics.Raycast(left.position, Vector3.down, 10f, brickLayer);
        }
        else if(state == MoveState.Right)
        {
            return Physics.Raycast(right.position, Vector3.down, 10f, brickLayer);
        }
        else
        {
            return false;
        }
    } 
    
    public void MovePlayer(MoveState state)
    {
        if(state == MoveState.Up)
        {
            transform.position += new Vector3(0, 0, 1);
        }
        else if(state == MoveState.Down)
        {
            transform.position += new Vector3(0, 0, -1);
        }
        else if(state == MoveState.Left)
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        else if(state == MoveState.Right)
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }
}
