using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;
    private bool isSwiping = false;

    public float swipeThreshold = 50f;

    private Vector3 moveDirection;

    private bool isMoving;

    private Stack<GameObject> bricks = new Stack<GameObject>();

    [SerializeField] GameObject Brick;
    [SerializeField] Transform playerTF;
    [SerializeField] float moveSpeed;

    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask brickLayer;
    [SerializeField] LayerMask lineLayer;
    [SerializeField] LayerMask endPointLayer;

    private float brickHeight = 0.2998985f;
    private float brickWidth = 1f;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (!isMoving)
        {
            CheckInput();
        }
        else
        {
            Move(moveDirection);
            CheckWall();
            CheckBrick();
            CheckLine();
            CheckEndPoint();
        }
    }

    private void OnInit()
    {
        isMoving = false;
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            endMousePosition = Input.mousePosition;
            moveDirection = GetSwipeDirection();
            isSwiping = false;

            //if (moveDirection != Vector3.zero)
            //{
            //    isMoving = true;
            //}

            if (moveDirection != Vector3.zero && !CheckWall())
            {
                isMoving = true;
            }
        }
    }

    private Vector3 GetSwipeDirection()
    {
        float deltaX = endMousePosition.x - startMousePosition.x;
        float deltaY = endMousePosition.y - startMousePosition.y;

        if (Mathf.Abs(deltaX) > swipeThreshold || Mathf.Abs(deltaY) > swipeThreshold)
        {
            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                return new Vector3(deltaX, 0, 0).normalized;
            }
            else
            {
                return new Vector3(0, 0, deltaY).normalized;
            }
        }
        else
        {
            return Vector3.zero;
        }
    }

    //private void Move(Vector3 moveDirection)
    //{
    //    transform.position += moveDirection * moveSpeed * Time.deltaTime;       
    //}

    private void Move(Vector3 moveDirection)
    {
        transform.Translate(moveDirection * Time.deltaTime);
    }

    IEnumerator Move()
    {
        yield return null;
    }

    //private void CheckWall()
    //{
    //    RaycastHit hit;
    //    Vector3 raycastPos = transform.position;

    //    if (Physics.Raycast(raycastPos, moveDirection, out hit, brickWidth / 2, wallLayer))
    //    {
    //        isMoving = false;
    //    }
    //}

    private bool CheckWall()
    {
        RaycastHit hit;
        Vector3 raycastPos = transform.position;
        raycastPos.y += 1f;

        if (Physics.Raycast(raycastPos + moveDirection, Vector3.down, out hit, 1f, wallLayer))
        {
            isMoving = false;
            return true;
        }

        return false;
    }

    private void CheckBrick()
    {
        RaycastHit hit;
        Vector3 raycastPos = transform.position;
        raycastPos.y += 1f;

        if (Physics.Raycast(raycastPos, Vector3.down, out hit, 1f, brickLayer))
        {
            hit.collider.enabled = false;
            AddBrick(hit.collider.gameObject);
        }
    }

    private void CheckLine()
    {
        RaycastHit hit;
        Vector3 raycastPos = transform.position;
        raycastPos.y += 1f;

        if (Physics.Raycast(raycastPos, Vector3.down, out hit, 1f, lineLayer))
        {
            hit.collider.enabled = false;
            if (RemoveBrick())
            {
                GameObject yellow = Instantiate(GameController.Ins.Yellow, hit.collider.transform);
                yellow.transform.localPosition += new Vector3(0, 0.1f, 0);
            }
        }
    }

    private void CheckEndPoint()
    {
        RaycastHit hit;
        Vector3 raycastPos = transform.position;
        raycastPos.y += 1f;

        if (Physics.Raycast(raycastPos, Vector3.down, out hit, 1f, endPointLayer))
        {           
            isMoving = false;
            ClearBrick();
        }
    }

    private void AddBrick(GameObject brickObj)
    {
        bricks.Push(brickObj);
        brickObj.transform.SetParent(Brick.transform, false);
        brickObj.transform.position = playerTF.position;

        Vector3 playerPos = playerTF.position;
        playerPos.y += brickHeight;
        playerTF.position = playerPos;
    }

    private bool RemoveBrick()
    {
        if (bricks.Count > 0)
        {       
            Destroy(bricks.Pop());

            Vector3 playerPos = playerTF.position;
            playerPos.y -= brickHeight;
            playerTF.position = playerPos;

            return true;
        }
        else
        {
            LevelManager.Ins.GameOver();
            return false;
        }     
    }

    private void ClearBrick()
    {
        Vector3 playerPos = playerTF.position;
        playerPos.y -= brickHeight * bricks.Count;
        playerTF.position = playerPos;

        bricks.Clear();
        if (Brick != null)
        {
            Destroy(Brick);
        }
        Brick = new GameObject("Brick");
        Brick.transform.SetParent(transform);
        Brick.transform.localPosition = Vector3.zero;  
    }
}
