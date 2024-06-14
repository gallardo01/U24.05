using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class MovingStackMaker : MonoBehaviour
{
    [SerializeField] Transform Hoang;

    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform center;

    [SerializeField] private LayerMask unbrickLayer;
    [SerializeField] private LayerMask BrickLayer;

    private List<GameObject> brickStack = new List<GameObject>();
    public GameObject stack;


    private bool isRunning = false;

    public float SpaceBrick = 0.3f;
    private float stackHeight = 0;
    private float BrickHeight;

    public enum MoveState
    {
        Up,
        Down,
        Left,
        Right,
        Center
    }
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isRunning)
        {
            if (CheckMoveStatus(MoveState.Up))
            {
                StartCoroutine(MovePlayer(MoveState.Up));
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && !isRunning)
        {
            if (CheckMoveStatus(MoveState.Down))
            {
                StartCoroutine(MovePlayer(MoveState.Down));
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && !isRunning)
        {
            if (CheckMoveStatus(MoveState.Left))
            {
                StartCoroutine(MovePlayer(MoveState.Left));
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && !isRunning)
        {
            if (CheckMoveStatus(MoveState.Right))
            {
                StartCoroutine(MovePlayer(MoveState.Right));
            }
        }
    }

    IEnumerator MovePlayer(MoveState state)
    {
        isRunning = true;

        if (CheckMoveStatus(state))
        {
            //gach - phat hien vien gach
            Brick();

            //xep vien gach



            MovePlayerDirection(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MovePlayer(state));
        }
        else 
        {
            isRunning = false;
        }
    }

    private void Brick() 
    {
        RaycastHit hit;
        if (Physics.Raycast(center.position, Vector3.down, out hit, 1.1f, BrickLayer))
        {
            GameObject brick = hit.collider.gameObject;
            Debug.Log("brick: " + brick.name);
            //Destroy(brick);
            if (!brickStack.Contains(brick)) 
            {
                SapxepBrick(brick);
            }
        }
    }

    private void SapxepBrick(GameObject brick) 
    {
        brickStack.Add(brick);
        brick.transform.SetParent(stack.transform);
        UpdateBrick();
    }

    private void UpdateBrick() 
    {
        BrickHeight = brickStack.Count * SpaceBrick;

        Vector3 playerPosition = Hoang.position;
        playerPosition.y = BrickHeight;
        Hoang.position = playerPosition;

        Vector3 brickPush = new Vector3(0, stackHeight, 0);
        for (int i = 0; i < brickStack.Count; i++)
        {
            brickStack[i].transform.localPosition = brickPush;
            brickPush.y += SpaceBrick;
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
        //else if (state == MoveState.Center) 
        //{
        //    Debug.DrawLine(transform.position, transform.position + Vector3.down * 1f, Color.red);
        //    return Physics.Raycast(transform.position, Vector3.down, 1f, BrickLayer);
        //}
        return false;
    }

    private void MovePlayerDirection(MoveState state)
    {
        if (state == MoveState.Up)
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
        else if (state == MoveState.Down)
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
        else if (state == MoveState.Left)
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if (state == MoveState.Right)
        {
            transform.position += new Vector3(0f, 0f, -1f);
        }
    }
}
