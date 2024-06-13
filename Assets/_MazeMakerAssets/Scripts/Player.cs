using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform up;
    [SerializeField] Transform dowm;

    private BrickStacking brickStacking;

    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;

    private void Awake()
    {
        brickStacking = GetComponent<BrickStacking>();
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
            brickStacking.AddBrick();
            StartCoroutine(Move(canMove, directionPoint, moveDirection));
        }
        else StopAllCoroutines();
    }
}
