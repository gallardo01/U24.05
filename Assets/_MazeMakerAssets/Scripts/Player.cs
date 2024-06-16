using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MoveState
{
    Up,
    Down,
    Left,
    Right
}

public class Player : MonoBehaviour
{
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform up;
    [SerializeField] Transform dowm;

    MoveState currentMoveState;
    private BrickStacking brickStacking;
    public bool canMove = true;

    private void Awake()
    {
        brickStacking = GetComponent<BrickStacking>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.A) && canMove)
        {
            StartCoroutine(Move(MoveState.Left));
        }
        if (Input.GetKeyDown(KeyCode.D) && canMove)
        {
            StartCoroutine(Move(MoveState.Right));
        }
        if(Input.GetKeyDown(KeyCode.W) && canMove)
        {
            StartCoroutine(Move(MoveState.Up));
        }
        if (Input.GetKeyDown(KeyCode.S) && canMove)
        {
            StartCoroutine(Move(MoveState.Down));
        }
    }

    IEnumerator Move(MoveState state)
    {
        yield return new WaitForSeconds(0.05f);

        canMove = false;
        currentMoveState = state;
        Debug.Log(currentMoveState.ToString());

        if (CheckMoveStatus(state))
        {
            brickStacking.StackChecking();
            MovePlayerDirection(state);
            StartCoroutine(Move(state));
        }
        else
        {
            canMove = true;
        }
    }
    private bool CheckMoveStatus(MoveState state)
    {
        if (state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, 5f, 1 << 7);
        }
        else if (state == MoveState.Down)
        {
            return Physics.Raycast(dowm.position, Vector3.down, 5f, 1 << 7);
        }
        else if (state == MoveState.Left)
        {
            return Physics.Raycast(left.position, Vector3.down, 5f, 1 << 7);
        }
        else if (state == MoveState.Right)
        {
            return Physics.Raycast(right.position, Vector3.down, 5f, 1 << 7);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {
            StopAllCoroutines();
            transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);

            List<MoveState> moveStates = new List<MoveState>() { MoveState.Up, MoveState.Down, MoveState.Left, MoveState.Right };
            for (int i = 0; i < moveStates.Count; i++)
            {
                if(!CheckMoveStatus(moveStates[i]))
                {
                    moveStates.RemoveAt(i);
                }
            }
            moveStates.Remove(ReverseState(currentMoveState));

            int randomNumber = Random.Range(0, moveStates.Count);
            StartCoroutine(Move(moveStates[moveStates.Count-1]));
        }

        if (other.gameObject.CompareTag("Win"))
        {
            GameController.Instance.EndLevel();
        }
        
    }

    public MoveState ReverseState(MoveState movestate)
    {
        MoveState reverseState = new MoveState();
        if (movestate == MoveState.Left) reverseState = MoveState.Right;
        else if (movestate == MoveState.Right) reverseState = MoveState.Left;
        else if (movestate == MoveState.Up) reverseState = MoveState.Down;
        else if (movestate == MoveState.Down) reverseState = MoveState.Up;
        return reverseState;
    }
}
