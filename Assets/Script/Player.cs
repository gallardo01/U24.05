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

    private bool isRunning = false;

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
        if(Input.GetKeyDown(KeyCode.UpArrow) && !isRunning)
        {
            if(CanMoveTo(MoveState.Up))
            {
                StartCoroutine(MovePlayer(MoveState.Up));
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRunning)
        {
            if (CanMoveTo(MoveState.Down))
            {
                StartCoroutine(MovePlayer(MoveState.Down));
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRunning)
        {
            if (CanMoveTo(MoveState.Left))
            {
                StartCoroutine(MovePlayer(MoveState.Left));
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isRunning)
        {
            if (CanMoveTo(MoveState.Right))
            {
                StartCoroutine(MovePlayer(MoveState.Right));
            }
        }
    }

    IEnumerator MovePlayer(MoveState state)
    {
        isRunning = true;
        if(CanMoveTo(state))
        {
            MovingTo(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MovePlayer(state));
        }
        else
        {
            isRunning = false;
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
    
    public void MovingTo(MoveState state)
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
