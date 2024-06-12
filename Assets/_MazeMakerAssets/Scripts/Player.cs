using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform up;
    [SerializeField] Transform dowm;
    [SerializeField] Transform brickPrefab;
    [SerializeField] Transform playerBody;

    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;

    // Stack Pop(), Push();
    Stack<Transform> stack = new Stack<Transform>();

    private void Start()
    {
        RayCheckToMove();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Move(canMoveLeft, left, -Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(Move(canMoveRight, right, Vector3.right));
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(Move(canMoveUp, up, Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Move(canMoveDown, dowm, -Vector3.forward));
        }
    }

    IEnumerator Move(bool canMove, Transform directionPoint, Vector3 moveDirection)
    {
        yield return new WaitForSeconds(0.05f);

        canMove = Physics.Raycast(directionPoint.position, Vector3.down, 5f, 1 << 7);
        if (canMove)
        {
            transform.Translate(moveDirection);
            StartCoroutine(Move(canMove, directionPoint, moveDirection));
        }
        else StopAllCoroutines();
    }

    private void RayCheckToMove()
    {
        canMoveLeft = Physics.Raycast(left.position, Vector3.down, 5f, 1 << 7);
        canMoveRight = Physics.Raycast(right.position, Vector3.down, 5f, 1 << 7);
        canMoveUp = Physics.Raycast(up.position, Vector3.down, 5f, 1 << 7);
        canMoveDown = Physics.Raycast(dowm.position, Vector3.down, 5f, 1 << 7);
    }

    private void RayCheckBrick()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 5f, 1 << 8))
        {

        }
    }

    private void AddBrick()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 5f, 1 << 8))
        {
            Transform newBrick = Instantiate(brickPrefab);
            stack.Push(newBrick);

            newBrick.SetParent(this.transform);
            newBrick.transform.localPosition = playerBody.transform.localPosition;
        }
    }

    private void RemoveBrick()
    {

    }
}
