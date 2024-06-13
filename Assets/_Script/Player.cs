using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform center;
    public InputAction moveLeft;
    public InputAction moveRight;
    public InputAction moveUp;
    public InputAction moveDown;
    RaycastHit hit, hitBrick;

    public GameObject brick;

    public enum MoveState
    {
        Up,
        Down,
        Left,
        Right,
    }
    // Start is called before the first frame update
    void Start()
    {
        moveDown.Enable();
        moveUp.Enable();
        moveRight.Enable();
        moveLeft.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft.triggered)
        {
            if (CheckTheRoad(MoveState.Left))
            {
                StartCoroutine(AutoMove(MoveState.Left));
            }
        }
        if (moveRight.triggered)
        {
            if (CheckTheRoad(MoveState.Right))
            {
                StartCoroutine(AutoMove(MoveState.Right));
            }
        }
        if (moveDown.triggered)
        {
            if (CheckTheRoad(MoveState.Down))
            {
                StartCoroutine(AutoMove(MoveState.Down));
            }
        }
        if (moveUp.triggered)
        {
            if (CheckTheRoad(MoveState.Up))
            {
                StartCoroutine(AutoMove(MoveState.Up));
            }
        }
    }
    private bool CheckTheRoad(MoveState state)
    {
        if (state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, out hit);
        }
        else if (state == MoveState.Down)
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
        }
        else
        {
            return false;
        }
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
            yield return new WaitForSeconds(5f);
            StartCoroutine(AutoMove(state));
        }
    }
}

