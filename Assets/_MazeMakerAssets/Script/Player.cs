    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    [SerializeField] private LayerMask unbrickLayer;

    public enum MoveState
    {
        Up,
        Down,
        Left,
        Right
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
            if (CheckMoveStatus(MoveState.Up))
            {
                StartCoroutine(MovePlayer(MoveState.Up));
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CheckMoveStatus(MoveState.Down))
            {
                StartCoroutine(MovePlayer(MoveState.Down));
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CheckMoveStatus(MoveState.Left))
            {
                StartCoroutine(MovePlayer(MoveState.Left));
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CheckMoveStatus(MoveState.Right))
            {
                StartCoroutine(MovePlayer(MoveState.Right));
            }
        }
    }

    IEnumerator MovePlayer(MoveState state)
    {
        if (CheckMoveStatus(state))
        {
            MovePlayerDirection(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MovePlayer(state));
        }
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
