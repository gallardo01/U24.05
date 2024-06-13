using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    public InputAction moveLeft;
    public InputAction moveRight;
    public InputAction moveUp;
    public InputAction moveDown;
    RaycastHit hit;

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
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            if (CheckTheRoad(MoveState.Left))
            {
                StartCoroutine(AutoMove(MoveState.Left));
            }         
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            if (CheckTheRoad(MoveState.Right))
            {
                StartCoroutine(AutoMove(MoveState.Right));
            }
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            if (CheckTheRoad(MoveState.Down))
            {
                StartCoroutine(AutoMove(MoveState.Down));
            }
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            if (CheckTheRoad(MoveState.Up))
            {
                StartCoroutine(AutoMove(MoveState.Up));
            }
        }
        //if (moveLeft.IsPressed() && canMoveLeft)
        //{
        //    MovePlayer(CheckTheRoad(left));
        //}
        //if (moveRight.IsPressed() && canMoveRight)
        //{
        //    MovePlayer(CheckTheRoad(right));
        //}
        //if (moveUp.IsPressed() && canMoveUp)
        //{
        //    MovePlayer(CheckTheRoad(up));
        //}
        //if (moveDown.IsPressed() && canMoveDown)
        //{
        //    MovePlayer(CheckTheRoad(down));
        //}
    }
    private bool CheckTheRoad(MoveState state)
    {
        if (state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, out hit);
        }
        else if(state == MoveState.Down)
        {
            return Physics.Raycast(down.position, Vector3.down, out hit);
        }
        else if (state == MoveState.Right)
        {
            return Physics.Raycast(right.position, Vector3.down, out hit);
        }

        else if (state == MoveState.Left)
        {
            return Physics.Raycast(left.position, Vector3.down, out hit);
        } else
        {
            return false;
        }


        // => Tao them Layer cho unbrick Prefabs
        // return Physics.Raycast(transform.position, Vector3.down, 1f, UnBrick);
    }

    private void MovePlayer(bool check)
    {
        if (check)
        {
            GameObject hitObject = hit.collider.gameObject;
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(hitObject.transform.position.x, currentPos.y, hitObject.transform.position.z);
        }
    }
    IEnumerator AutoMove(MoveState state)
    {
        if (CheckTheRoad(state))
        {
            MovePlayer(CheckTheRoad(state));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(AutoMove(state));
        }
    }
}

