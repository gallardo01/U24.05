    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform center;
    [SerializeField] Transform player;

    [SerializeField] Transform bricksParent;
    [SerializeField] private LayerMask unbrickLayer;
    [SerializeField] private LayerMask brickLayer;

    private bool isRunning = false;
    private int totalBrick = 0;
    public enum MoveState
    {
        Up,
        Down,
        Left,
        Right,
        Center
    }
    // Start is called before the first frame update
    void Start()
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
    }

    IEnumerator MovePlayer(MoveState state)
    {
        isRunning = true;
        if (CheckMoveStatus(state))
        {
            RaycastHit hit;
            if (Physics.Raycast(center.transform.position, Vector3.down, out hit, 5f, brickLayer))
            {
                totalBrick++;
                hit.collider.gameObject.transform.SetParent(bricksParent);
                hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                hit.collider.transform.localPosition = new Vector3(0f, 0.25f * (totalBrick-2), 0f);
                player.transform.localPosition = new Vector3(0f, -0.15f + 0.25f*(totalBrick-1), 0f);
            }
            // Trang'

            MovePlayerDirection(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MovePlayer(state));
        } else
        {
            // Tim' 
                
            isRunning = false;
        }
    }

    private MoveState FindPushDirection(MoveState currentState)
    {
        // currentState = up

        // check 4 huong o diem hien tai - huong nao di dc
        // down, left

        return currentState;
    }
    private bool CheckMoveStatus(MoveState state)
    {
        if (state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, 1f, unbrickLayer);
        }
        else if (state == MoveState.Down)
        {
            return Physics.Raycast(down.position, Vector3.down, 1f, unbrickLayer);
        }
        else if (state == MoveState.Left)
        {
            return Physics.Raycast(left.position, Vector3.down, 1f, unbrickLayer);
        }
        else if (state == MoveState.Right)
        {
            return Physics.Raycast(right.position, Vector3.down, 1f, unbrickLayer);
        }
        return false;
    }

    private void MovePlayerDirection(MoveState state)
    {
        if (state == MoveState.Up)
        {
            transform.position += new Vector3(0f, 0f, -1f);
        }
        else if (state == MoveState.Down)
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if (state == MoveState.Left)
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
        else if (state == MoveState.Right)
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
    }
}
