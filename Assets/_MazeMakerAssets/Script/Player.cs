    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform center;
    [SerializeField] Transform player;

    [SerializeField] Transform bricksParent;
    [SerializeField] private LayerMask unbrickLayer;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask pushLayer;
    [SerializeField] private LayerMask whiteLayer;
    [SerializeField] Animator animator;

    private bool isRunning = false;
    private int totalBrick = 0;
    private List<GameObject> bricks = new List<GameObject>();
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CheckMoveStatus(MoveState.Up) && !isRunning)
            {
                StartCoroutine(MovePlayer(MoveState.Up));
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CheckMoveStatus(MoveState.Down) && !isRunning)
            {
                StartCoroutine(MovePlayer(MoveState.Down));
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CheckMoveStatus(MoveState.Left) && !isRunning)
            {
                StartCoroutine(MovePlayer(MoveState.Left));
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CheckMoveStatus(MoveState.Right) && !isRunning)
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
            RaycastHit hit;
            if (Physics.Raycast(center.transform.position, Vector3.down, out hit, 5f, brickLayer))
            {
                totalBrick++;
                hit.collider.gameObject.transform.SetParent(bricksParent);
                hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                hit.collider.transform.localPosition = new Vector3(0f, 0.25f * (totalBrick-2), 0f);
                player.transform.localPosition = new Vector3(0f, -0.15f + 0.25f*(totalBrick-1), 0f);
                bricks.Add(hit.collider.gameObject);
            }
            // Trang'
            RaycastHit hitWhite;
            if (Physics.Raycast(center.transform.position, Vector3.down, out hitWhite, 5f, whiteLayer))
            {
                if (bricks.Count > 0)
                {
                    bricks[totalBrick - 1].transform.SetParent(hitWhite.collider.gameObject.transform);
                    bricks[totalBrick - 1].transform.localPosition = Vector3.zero;
                    totalBrick--;
                    player.transform.localPosition = new Vector3(0f, -0.15f + 0.25f * (totalBrick - 1), 0f);
                }
                else
                {
                    // thua
                }
            }
            MovePlayerDirection(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MovePlayer(state));
        } else
        {
            // Tim' 
            isRunning = false;
            if (Physics.Raycast(center.transform.position, Vector3.down, 5f, pushLayer))
            {
                MoveState newDirection = FindPushDirection(state);
                StartCoroutine(MovePlayer(newDirection));
            }
        }
    }

    private MoveState FindPushDirection(MoveState currentState)
    {
        // check 4 huong o diem hien tai - huong nao di dc
        if (CheckMoveStatus(MoveState.Up) && currentState != MoveState.Down)
        {
            return MoveState.Up;
        } 
        else if (CheckMoveStatus(MoveState.Down) && currentState != MoveState.Up)
        {
            return MoveState.Down;
        }
        else if (CheckMoveStatus(MoveState.Left) && currentState != MoveState.Right)
        {
            return MoveState.Left;
        }
        else if (CheckMoveStatus(MoveState.Right) && currentState != MoveState.Left)
        {
            return MoveState.Right;
        }
        return currentState;
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
        return false;
    }

    private void MovePlayerDirection(MoveState state)
    {
        if (state == MoveState.Up)
        {
            transform.position += new Vector3(0f, 0f, -1f);
        }
        else if (state == MoveState.Down)
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if (state == MoveState.Left)
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
        else if (state == MoveState.Right)
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Final")
        {
            player.transform.localPosition = new Vector3(0f, -0.15f, 0f);
            bricksParent.gameObject.SetActive(false);

            transform.SetParent(collision.gameObject.transform);
            transform.localPosition = new Vector3(0f, 2.5f, 5f);
            animator.SetInteger("renwu", 2);
        }
    }
}
