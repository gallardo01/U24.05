using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] private LayerMask unbrickLayer;
    private bool isRunning = false;
    [SerializeField] private LayerMask brickLayer; 
    [SerializeField] private LayerMask pushLayer; 
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private Transform stack;
    [SerializeField] private Transform player;

    public enum MoveState
    {
           Up,
        Down,
        Left,
        Right
    }
   
    //public bool canMoveUp = false;
    //public bool canMoveDown = false;
    //public bool canMoveLeft = false;
    //public bool canMoveRight = false;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CheckMoveStatus(MoveState.Up) && !isRunning) 
            {
                StartCoroutine(MovePlayer(MoveState.Up));
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CheckMoveStatus(MoveState.Down) && !isRunning)
            {
                StartCoroutine(MovePlayer(MoveState.Down));
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CheckMoveStatus(MoveState.Left) && !isRunning)
            {
                StartCoroutine(MovePlayer(MoveState.Left));
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CheckMoveStatus(MoveState.Right) && !isRunning)
            {
                StartCoroutine(MovePlayer(MoveState.Right));
            }
        }
        
        
        
        // if (IsTouchingBrick())
        // {
        //     PickUpBrick();
        // }
    }
    
    IEnumerator MovePlayer(MoveState state)
    {
        isRunning = true;
        if (CheckMoveStatus(state))
        {
            CheckAndPickBrick();
            //trang
            MovePlayerDirection(state);
            CheckAndPickBrick();
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MovePlayer(state));
        }
        else
        {
            //phat hien mau tim
            // xac dinh huong di tiep theo
            isRunning = false;
        }
    }

    private MoveState FindPushDirection(MoveState currentState)
    {
        //  current State = up
        // check 4 huong o diem hien tai huong nao di dc
        // down , left
        return currentState;
    }

    bool CheckMoveStatus(MoveState state)
    {

        if (state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, 1f, unbrickLayer);
        } else if (state == MoveState.Down)
        {
            return Physics.Raycast(down.position, Vector3.down, 1f, unbrickLayer);
        } else if (state == MoveState.Left)
        {
            return Physics.Raycast(left.position, Vector3.down, 1f, unbrickLayer);
        } else if (state == MoveState.Right)
        {
            return Physics.Raycast(right.position, Vector3.down, 1f, unbrickLayer);
        }
        return false;
    }

    private void MovePlayerDirection(MoveState state)
    {
        if (state == MoveState.Up)
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if (state == MoveState.Down)
        {
            transform.position += new Vector3(0f, 0f, -1f);
        }
        else if (state == MoveState.Left)
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
        else if (state == MoveState.Right)
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
    }
    
    void CheckAndPickBrick()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, brickLayer))
        {               
            GameObject brick = hit.collider.gameObject;
            if (brick.CompareTag("Brick"))
            {
                brick.transform.position = transform.position - new Vector3(0, 0.25f, 0);
                brick.transform.position += new Vector3(0, stack.childCount * 0.25f, 0);
                player.transform.position = up.transform.position + new Vector3(0, 0.25f * (stack.childCount - 1) , -1);
                brick.transform.parent = stack;

            }
        }
    }
    
    
}
