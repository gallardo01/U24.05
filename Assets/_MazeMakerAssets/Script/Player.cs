using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] private LayerMask unbrickLayer;
    private bool isRunning = false;
    [SerializeField] private LayerMask brickLayer; 
    [SerializeField] private LayerMask pushLayer; 
    [SerializeField] private LayerMask whiteLayer;

    [SerializeField] private Transform stack;
    [SerializeField] private Transform player;
    [SerializeField] private Transform drop;
    [SerializeField] Animator animator;
    [SerializeField] private AudioSource pickBrickSound;

    private int totalBrick = 0;
    private List<GameObject> bricks = new List<GameObject>();

    public enum MoveState
    {
           Up,
        Down,
        Left,
        Right
    }
   
    //public bool canMoveUp = false;
    //public bool canMoveDown = false;
    //public bool canMoveLeft = false;
    //public bool canMoveRight = false;

    private void Start()
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
        
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1f, Color.red);
        
        
        
        // if (IsTouchingBrick())
        // {
        //     PickUpBrick();
        // }
    }
    
    IEnumerator MovePlayer(MoveState state)
    {
        isRunning = true;
        if (CheckMoveStatus(state))
        {
            CheckAndPickBrick();
            //trang
            MovePlayerDirection(state);
            CheckAndPickBrick();
            CheckWhiteAndDropBrick();
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(MovePlayer(state));
        }
        else
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 5f, pushLayer))
            {
                MoveState newDirection = FindPushDirection(state);
                StartCoroutine(MovePlayer(newDirection));
                //push
            }
            //phat hien mau tim
            // xac dinh huong di tiep theo
            isRunning = false;
        }
    }

    private MoveState FindPushDirection(MoveState currentState)
    {
        
        if(CheckMoveStatus(MoveState.Up) && currentState != MoveState.Down)
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
        //  current State = up
        // check 4 huong o diem hien tai huong nao di dc
        // down , left
        return currentState;
    }

    bool CheckMoveStatus(MoveState state)
    {

        if (state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, 1f, unbrickLayer);
        } else if (state == MoveState.Down)
        {
            return Physics.Raycast(down.position, Vector3.down, 1f, unbrickLayer);
        } else if (state == MoveState.Left)
        {
            return Physics.Raycast(left.position, Vector3.down, 1f, unbrickLayer);
        } else if (state == MoveState.Right)
        {
            return Physics.Raycast(right.position, Vector3.down, 1f, unbrickLayer);
        }
        return false;
    }

    private void MovePlayerDirection(MoveState state)
    {
        if (state == MoveState.Up)
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if (state == MoveState.Down)
        {
            transform.position += new Vector3(0f, 0f, -1f);
        }
        else if (state == MoveState.Left)
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
        else if (state == MoveState.Right)
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Final"))
        {
            player.transform.position = up.transform.position + new Vector3(0, 0.25f * (stack.childCount - 1), -2);
            animator.SetInteger("renwu", 2);
        }   
    }

    void CheckAndPickBrick()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, brickLayer))
        {
            GameObject brick = hit.collider.gameObject;
         
            if (brick.CompareTag("Brick"))
            {
                // If the brick is a normal brick, pick it up
                //brick.transform.position = transform.position - new Vector3(0, 0.25f, 0);
                //brick.transform.position += new Vector3(0, stack.childCount * 0.25f, 0);
                //player.transform.position = up.transform.position + new Vector3(0, 0.25f * (stack.childCount - 1) , -1);
                //brick.transform.parent = stack;
                totalBrick++;
                pickBrickSound.Play();

                brick.transform.SetParent(stack);
                brick.GetComponent<BoxCollider>().enabled = false;
                brick.transform.position = transform.position - new Vector3(0, 0.25f, 0);
                brick.transform.position += new Vector3(0, stack.childCount * 0.25f, 0);
                player.transform.position = up.transform.position + new Vector3(0, 0.25f * (stack.childCount - 1), -1);
                //brick.transform.localPosition = new Vector3(0f, 0.25f * (totalBrick - 2), 0f);
                //player.transform.position = new Vector3(0f, -0.15f + 0.25f * (totalBrick - 1), 0f);
                bricks.Add(brick);
            }
        }
    }
    
    void CheckWhiteAndDropBrick()
    {
        RaycastHit hitWhite;
        if (Physics.Raycast(transform.position, Vector3.down, out hitWhite, 5f, whiteLayer))
        {
            GameObject whiteBrick = hitWhite.collider.gameObject;

            if (whiteBrick.CompareTag("White"))
            {
                if (stack.childCount > 0)
                {
                    Transform brickToDrop = stack.GetChild(stack.childCount - 1);
                    brickToDrop.parent = whiteBrick.transform;
                    Debug.Log("Drop");
                    brickToDrop.position = whiteBrick.transform.position;
                    player.transform.position = up.transform.position + new Vector3(0, 0.25f * (stack.childCount - 1), -1);
                }
            }
            //if(bricks.Count > 0)
            //{
            //    bricks[totalBrick - 1].transform.SetParent(whiteBrick.transform);
            //    bricks[totalBrick - 1].transform.position = whiteBrick.transform.position + new Vector3(0, 0.25f * hitWhite.collider.gameObject.transform.childCount, 0);
            //    totalBrick--;
            //    player.transform.position = up.transform.position + new Vector3(0, 0.25f * (stack.childCount - 1) , -1);
            //}

            //else
            //{
            //    Debug.Log("No brick to drop");
            //}       
        }
    }
    
}
