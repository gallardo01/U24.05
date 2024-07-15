using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    public Animator animator;
    public string currentAnimName = "idle";
    public Transform mesh;
    public float speed;
    public int colorIndex;
    public SkinnedMeshRenderer body;
    public LayerMask groundLayer;
    public LayerMask stairLayer;

    public GameObject stack;
    public int totalBrick;
    public StageController stage;

    private List<GameObject> brickStack = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacterColor(int color)
    {
        colorIndex = color;
        body.material = ColorController.Instance.GetMaterialColor(colorIndex);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName && currentAnimName != "victory")
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    protected void RemoveBrick()
    {
        if (totalBrick > 0)
        {
            Destroy(brickStack[totalBrick - 1].gameObject);
            brickStack.RemoveAt(totalBrick - 1);
            totalBrick--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick") && colorIndex == other.gameObject.GetComponent<Brick>().brickColor)
        {
            //Debug.Log("Da va cham voi brick");
            GameObject brick = other.gameObject;
            if (!brickStack.Contains(brick))
            {
                brick.transform.SetParent(stack.transform);

                brick.transform.localRotation = Quaternion.Euler(0, 90, 0);

                float stackHeight = brickStack.Count * brick.transform.localScale.y * 1.5f;
                Vector3 offset = new Vector3(0, stackHeight, -brick.transform.localScale.z);
                brick.transform.localPosition = offset;
                brick.GetComponent<Brick>().RemoveBricks(); 
                brickStack.Add(brick);
                totalBrick++;

                other.gameObject.GetComponent<Brick>().stage.CreateNewBrick(other.GetComponent<Brick>().brickPosition);
            }
        }

        if (other.gameObject.CompareTag("Stage"))
        {
            this.stage = other.gameObject.GetComponent<Stage>().stage;
            other.gameObject.GetComponent<Stage>().stage.CharacterStartGame(colorIndex);
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("cham");
            GameController.Instance.EndGame(this);
        }
    }

    public bool CanMove(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 9f, stairLayer))
        {
            int stairColor = hit.collider.gameObject.GetComponent<Stair>().stairColor;
            if (colorIndex != stairColor)
            {
                //Check con gach hay khong
                if (totalBrick > 0)
                {
                    RemoveBrick();
                    //Tha gach
                    hit.collider.gameObject.GetComponent<Stair>().SetStairColor(colorIndex);
                }
                return false;
            }
        }
        return Physics.Raycast(nextPoint, Vector3.down, groundLayer);
    }
}
