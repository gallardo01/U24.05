using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTwo : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask brickLayer;

    private Vector3 targetPosition;
    private bool isMoving = false;

    public enum Direct { None, Forward, Back, Right, Left }

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 startMousePosition = Input.mousePosition;

            // Listen for mouse button release to determine swipe direction
            StartCoroutine(DetectSwipe(startMousePosition));
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private IEnumerator DetectSwipe(Vector3 startMousePosition)
    {
        // Wait until the mouse button is released
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }

        Vector3 endMousePosition = Input.mousePosition;
        Vector3 swipeDirection = endMousePosition - startMousePosition;
        float minSwipeDistance = 50f; // Minimum swipe distance to trigger an action

        if (swipeDirection.magnitude >= minSwipeDistance)
        {
            Direct direction = Direct.None;
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x > 0)
                {
                    // Swipe right
                    direction = Direct.Right;
                }
                else
                {
                    // Swipe left
                    direction = Direct.Left;
                }
            }
            else
            {
                if (swipeDirection.y > 0)
                {
                    // Swipe up
                    direction = Direct.Forward;
                }
                else
                {
                    // Swipe down
                    direction = Direct.Back;
                }
            }

            DetermineTargetPosition(direction);
        }
    }

    private void DetermineTargetPosition(Direct direction)
    {
        Vector3 directionVector = Vector3.zero;

        switch (direction)
        {
            case Direct.Forward:
                directionVector = Vector3.forward;
                break;
            case Direct.Back:
                directionVector = Vector3.back;
                break;
            case Direct.Right:
                directionVector = Vector3.right;
                break;
            case Direct.Left:
                directionVector = Vector3.left;
                break;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionVector, out hit, 5f, brickLayer))
        {
            targetPosition = hit.point;
            isMoving = true;
            Debug.Log("Đã xác định điểm đến: " + targetPosition);
        }
    }

    private void MoveToTarget()
    {
        float step = 5f * Time.deltaTime; // Tốc độ di chuyển
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            isMoving = false;
        }
    }
}
