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

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
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

                //StageController.Instance.CreateNewBrick(other.GetComponent<Brick>().brickPosition);
                other.gameObject.GetComponent<Brick>().stage.CreateNewBrick(other.GetComponent<Brick>().brickPosition);
            }
        }

        if (other.CompareTag("Stage"))
        {
            //debug.log("da va cham voi stage 2");
            //StageController.Instance.CharacterStartGame(colorIndex);
            this.stage = other.gameObject.GetComponent<Stage>().stage;
            other.gameObject.GetComponent<Stage>().stage.CharacterStartGame(colorIndex);
        }
    }
}
