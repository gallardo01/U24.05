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
    public bool canMove = true;

    private void Awake()
    {
        brickStacking = GetComponent<BrickStacking>();
    }

    private void Update()
    {
        if (!canMove) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Move(left, -Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(Move(right, Vector3.right));
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(Move(up, Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Move(dowm, -Vector3.forward));
        }
    }

    IEnumerator Move(Transform directionPoint, Vector3 moveDirection)
    {
        yield return new WaitForSeconds(0.05f);

        bool canMoveThisDirect = Physics.Raycast(directionPoint.position, Vector3.down, 5f, 1 << 7);
        if (canMoveThisDirect)
        {
            canMove = false;
            transform.Translate(moveDirection);
            brickStacking.StackChecking();
            StartCoroutine(Move(directionPoint, moveDirection));
        }
        else
        {
            canMove = true;
        }
    }
}
