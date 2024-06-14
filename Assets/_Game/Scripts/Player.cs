using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField] float moveTime;

    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask brickLayer;
    [SerializeField] LayerMask lineLayer;
    [SerializeField] LayerMask endPointLayer;

    private float brickHeight = 0.2998985f;

    private void Update()
    {
        if (!isMoving)
        {
            CheckInput();
        }
    }

    public void OnInit(Transform startPos)
    {
        isMoving = false;
        transform.position = startPos.position;
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

            if (moveDirection != Vector3.zero)
            {
                isMoving = true;
                MoveNext(moveDirection);
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

    private void MoveNext(Vector3 moveDirection)
    {
        CheckWall();

        if (!isMoving)
        {
            return;
        }

        transform.DOMove(transform.position + moveDirection, moveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            CheckBrick();
            CheckLine();
            CheckEndPoint();
            MoveNext(moveDirection);
        });
    }

    private void CheckWall()
    {
        Vector3 raycastPos = transform.position;
        raycastPos.y += 1f;

        if (Physics.Raycast(raycastPos + moveDirection, Vector3.down, 1f, wallLayer))
        {
            isMoving = false;
        }
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
            if (RemoveBrick())
            {
                hit.collider.enabled = false;
                GameObject yellow = Instantiate(GameManager.Ins.Yellow, hit.collider.transform);
                yellow.transform.localPosition += new Vector3(0, 0.1f, 0);
            }
            else
            {
                isMoving = false;
            }
        }
    }

    private void CheckEndPoint()
    {
        Vector3 raycastPos = transform.position;
        raycastPos.y += 1f;

        if (Physics.Raycast(raycastPos, Vector3.down, 1f, endPointLayer))
        {           
            isMoving = false;
            ClearBrick();
            UIManager.Ins.OpenUI<UIWin>();
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

        GameManager.Ins.AddScore(10);
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
            UIManager.Ins.OpenUI<UILose>();
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
