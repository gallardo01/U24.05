using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform center;
    //[SerializeField] Transform checkPoint;

    [SerializeField] private LayerMask unbrickLayer;
    [SerializeField] private LayerMask brickLayer;

    public GameObject brickPrefabs;
    public GameObject stack;

    private bool isMoving = false;
    private int brickCount = 0;
    private List<GameObject> brickList = new List<GameObject>();

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
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CheckMoveStatus(MoveState.Up))
                {
                    StartCoroutine(MovePlayer(MoveState.Up));
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (CheckMoveStatus(MoveState.Down))
                {
                    StartCoroutine(MovePlayer(MoveState.Down));
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (CheckMoveStatus(MoveState.Left))
                {
                    StartCoroutine(MovePlayer(MoveState.Left));
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (CheckMoveStatus(MoveState.Right))
                {
                    StartCoroutine(MovePlayer(MoveState.Right));
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    if (CheckMoveStatus(MoveState.Up))
        //    {
        //        StartCoroutine(MovePlayer(MoveState.Up));
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    if (CheckMoveStatus(MoveState.Down))
        //    {
        //        StartCoroutine(MovePlayer(MoveState.Down));
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    if (CheckMoveStatus(MoveState.Left))
        //    {
        //        StartCoroutine(MovePlayer(MoveState.Left));
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    if (CheckMoveStatus(MoveState.Right))
        //    {
        //        StartCoroutine(MovePlayer(MoveState.Right));
        //    }
        //}  
    }

    IEnumerator MovePlayer(MoveState state)
    {
        while (CheckMoveStatus(state))
        {
            isMoving = true;
            if (isMoving == true)
            {
                //Debug.Log("State: " + state);

                //Phat hien vien gach
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, brickLayer))
                {
                    GameObject brick = hit.collider.gameObject;
                    //Debug.Log("Detected brick: " + hit.collider.name);
                    brickCount++;
                    //Debug.Log("Brick count: " + brickCount);
                    //Destroy(hit.collider.gameObject);
                    //StartCoroutine(CreateBrick());

                    //Nhat gach len
                    AddBrick(brick);
                }
                MovePlayerDirection(state);
                yield return new WaitForSeconds(0.1f);
            }
        }
        isMoving = false;
        
        //if (CheckMoveStatus(state))
        //{
        //    Debug.Log("State: " +  state);
        //    MovePlayerDirection(state);
        //    yield return new WaitForSeconds(0.1f);
        //    StartCoroutine(MovePlayer(state));
        //}
    }

    private void AddBrick(GameObject brick)
    {
        brickList.Add(brick);   
        brick.transform.SetParent(stack.transform);
        UpdateBrick();
    }

    private void UpdateBrick()
    {
        BrickHeight = brickList.Count * SpaceBrick;

        Vector3 playerPosition = playerTransform.position;
        playerPosition.y = BrickHeight;
        playerTransform.position = playerPosition;

        Vector3 brickPush = new Vector3(0, stackHeight, 0);
        for (int i = 0; i < brickList.Count; i++)
        {
            brickList[i].transform.localPosition = brickPush;
            brickPush.y += SpaceBrick;
        }
    }

    //IEnumerator CreateBrick()
    //{ 
    //    //yield return new WaitForSeconds(0.1f);
    //    GameObject brick = Instantiate(brickPrefabs, transform.position, Quaternion.identity);
    //    if (brickList.Count > 0)
    //    {
    //        GameObject lastBrick = brickList[brickList.Count - 1];
    //        brick.transform.position = lastBrick.transform.position + new Vector3(0, 0.5f, 0);
    //    }
    //    else 
    //    {
    //        brick.transform.position = transform.position - new Vector3(0, 0.5f, 0);
    //    } 
    //    brick.transform.SetParent(transform);
    //    brickList.Add(brick);
    //    yield return new WaitForSeconds(0.1f);
    //    brick.transform.localPosition = new Vector3(0, 0.25f, 0);
    //    //transform.position += new Vector3(0, 0.25f, 0);
    //    //StartCoroutine(CreateBrick());
    //    Debug.Log("Brick list count: " + brickList.Count);
    //}

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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Brick"))
    //    {
    //        Debug.Log("Da va cham voi brick");
    //        brickCount++;

    //        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    //        Debug.Log("Count brick: " + brickCount);
    //    }
    //}
}
