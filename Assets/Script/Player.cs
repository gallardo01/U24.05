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
    [SerializeField] LayerMask unBrickLayer;
    [SerializeField] LayerMask brickLayer;
    [SerializeField] LayerMask pushLayer;
    [SerializeField] LayerMask whiteLayer;
    
    [SerializeField] List<GameObject> bricks = new List<GameObject>();

    private bool isRunning = false;
    private int totalBrick = 0;

    public enum MoveState
    {
        Up,
        Down,
        Left,
        Right, 
        Center
    }

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && !isRunning)
        {
            if(CanMoveTo(MoveState.Up))
            {
                StartCoroutine(MovePlayer(MoveState.Up));
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRunning)
        {
            if (CanMoveTo(MoveState.Down))
            {
                StartCoroutine(MovePlayer(MoveState.Down));
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRunning)
        {
            if (CanMoveTo(MoveState.Left))
            {
                StartCoroutine(MovePlayer(MoveState.Left));
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isRunning)
        {
            if (CanMoveTo(MoveState.Right))
            {
                StartCoroutine(MovePlayer(MoveState.Right));
            }
        }
    }

    IEnumerator MovePlayer(MoveState state)
    {
        isRunning = true;
        if(CanMoveTo(state))
        {
            RaycastHit hit;
            if(Physics.Raycast(center.transform.position, Vector3.down, out hit, 10f, brickLayer))
            {
                totalBrick++;
                hit.collider.gameObject.transform.SetParent(bricksParent);
                hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                hit.collider.transform.localPosition = new Vector3(0f, 0.25f * (totalBrick - 1), 0f);
                player.transform.localPosition = new Vector3(0f, -0.15f + 0.25f * totalBrick, 0f);
                bricks.Add(hit.collider.gameObject);
            }

            RaycastHit hitWhite;
            if (Physics.Raycast(center.transform.position, Vector3.down, out hitWhite, 10f, whiteLayer))
            {
                if(totalBrick > 0)
                {
                    bricks[totalBrick - 1].transform.SetParent(hitWhite.collider.gameObject.transform);
                    bricks[totalBrick - 1].transform.localPosition = Vector3.zero;
                    totalBrick--;
                    player.transform.localPosition = new Vector3(0f, -0.15f + 0.25f * totalBrick, 0f);
                }
                else
                {
                    //lost
                }
            }


            MovingTo(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MovePlayer(state));
        }
        else
        {
            isRunning = false;
            if (Physics.Raycast(center.transform.position, Vector3.down, 10f, pushLayer))
            {
                MoveState newDirection = FindPushDirection(state);
                StartCoroutine(MovePlayer(newDirection));
            }
        }    
    }    

    public bool CanMoveTo(MoveState state)
    {
        if(state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, 10f, unBrickLayer);
        }
        else if(state == MoveState.Down)
        {
            return Physics.Raycast(down.position, Vector3.down, 10f, unBrickLayer);
        }
        else if(state == MoveState.Left)
        {
            return Physics.Raycast(left.position, Vector3.down, 10f, unBrickLayer);
        }
        else if(state == MoveState.Right)
        {
            return Physics.Raycast(right.position, Vector3.down, 10f, unBrickLayer);
        }
        else
        {
            return false;
        }
    } 
    
    public void MovingTo(MoveState state)
    {
        if(state == MoveState.Up)
        {
            transform.position += new Vector3(0, 0, 1);
        }
        else if(state == MoveState.Down)
        {
            transform.position += new Vector3(0, 0, -1);
        }
        else if(state == MoveState.Left)
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        else if(state == MoveState.Right)
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    public bool CanCollectBrick()
    {
        return Physics.Raycast(transform.position, Vector3.down, 10f, brickLayer);
    }    

    private MoveState FindPushDirection(MoveState currentState)
    {
        if(CanMoveTo(MoveState.Up) && currentState != MoveState.Down)
        {
            return MoveState.Up;
        }
        else if (CanMoveTo(MoveState.Down) && currentState != MoveState.Up)
        {
            return MoveState.Down;
        }
        if (CanMoveTo(MoveState.Left) && currentState != MoveState.Right)
        {
            return MoveState.Left;
        }
        if (CanMoveTo(MoveState.Right) && currentState != MoveState.Left)
        {
            return MoveState.Right;
        }
        return currentState;
    }
}
