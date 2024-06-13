using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStackMaker : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask Grounded;
    [SerializeField] private LayerMask BrickLayer;

    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    public bool canUp = false;
    public bool canDown = false;
    public bool canLeft = false;
    public bool canRight = false;

    public float speed;
    private bool isMoving = false;

    private List<GameObject> brickStack = new List<GameObject>();
    private float objectHeight = 0.3f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        canUp = CheckIsMoveStep(up);
        canDown = CheckIsMoveStep(down);
        canLeft = CheckIsMoveStep(left);
        canRight = CheckIsMoveStep(right);

        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) && canUp)
            {
                StartCoroutine(MoveContinuously(new Vector3(1, 0, 0)));
            }
            else if (Input.GetKeyDown(KeyCode.S) && canDown)
            {
                StartCoroutine(MoveContinuously(new Vector3(-1, 0, 0)));
            }
            else if (Input.GetKeyDown(KeyCode.A) && canLeft)
            {
                StartCoroutine(MoveContinuously(new Vector3(0, 0, 1)));
            }
            else if (Input.GetKeyDown(KeyCode.D) && canRight)
            {
                StartCoroutine(MoveContinuously(new Vector3(0, 0, -1)));
            }
        }

    }

    private bool CheckIsMoveStep(Transform DirectionPoint)
    {
        Debug.DrawLine(DirectionPoint.position, DirectionPoint.position + Vector3.down * 1.1f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(DirectionPoint.position, Vector3.down, out hit, 1.1f, Grounded))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Moving(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
        CheckForBricks();
    }

    private IEnumerator MoveContinuously(Vector3 direction)
    {
        isMoving = true;

        while (true)
        {
            if (direction == new Vector3(1, 0, 0) && canUp)
            {
                Moving(new Vector3(1, 0, 0));
            }
            else if (direction == new Vector3(-1, 0, 0) && canDown)
            {
                Moving(new Vector3(-1, 0, 0));
            }
            else if (direction == new Vector3(0, 0, 1) && canLeft)
            {
                Moving(new Vector3(0, 0, 1));
            }
            else if (direction == new Vector3(0, 0, -1) && canRight)
            {
                Moving(new Vector3(0, 0, -1));
            }
            else
            {
                break;
            }

            // Cập nhật lại các trạng thái di chuyển
            canUp = CheckIsMoveStep(up);
            canDown = CheckIsMoveStep(down);
            canLeft = CheckIsMoveStep(left);
            canRight = CheckIsMoveStep(right);

            yield return null;
        }

        isMoving = false;
    }

    private void CheckForBricks()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, BrickLayer))
        {
            GameObject brick = hit.collider.gameObject;
            Debug.Log("cham voi brick");
            if (!brickStack.Contains(brick))
            {
                CollectBrick(brick);
            }
        }
    }


    private void CollectBrick(GameObject brick)
    {
        brickStack.Add(brick);
        brick.transform.SetParent(transform);
        updatebrickpositions();
        UpdatePlayerPosition();

    }

    private void updatebrickpositions()
    {
        //for (int i = 0; i < brickStack.Count; i++)
        //{
        //    brickStack[i].transform.localPosition = new Vector3(0, -1 - i, 0);
        //}

        float stackHeight = 0f; 
        for (int i = 0; i < brickStack.Count; i++)
        {
            Vector3 brickPos = new Vector3(0, -stackHeight, 0); // Tính toán vị trí mới cho viên gạch
            brickStack[i].transform.localPosition = brickPos; // Cập nhật vị trí của viên gạch

            stackHeight += objectHeight; // Tăng độ cao của cột viên gạch lên theo chiều dọc
        }

    }

    private void UpdatePlayerPosition()
    {
        float newyposition = objectHeight * brickStack.Count;
        transform.localPosition = new Vector3(transform.localPosition.x, newyposition, transform.localPosition.z);
    }
}
