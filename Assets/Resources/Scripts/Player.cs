using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //[SerializeField] Transform playerTransform;

    //public Animator animator;
    //public string currentAnimName = "idle";
    //public Transform mesh;
    //public float speed;
    //public int colorIndex;
    //public SkinnedMeshRenderer body;
    //public LayerMask groundLayer;
    //public LayerMask stairLayer;

    //public GameObject stack;
    //public int totalBrick;

    //private List<GameObject> brickStack = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;   
        
        if(direction.magnitude > 0f)
        {
            //transform.Translate(direction * speed * Time.deltaTime);
            Vector3 nextPoint = transform.position + JoystickControl.direct * Time.deltaTime * speed;
            
            if (CanMove(nextPoint))
            {
                transform.position = nextPoint; 
            }
            ChangeAnim("run");
            mesh.forward = JoystickControl.direct;
        }
        else
        {
            ChangeAnim("idle");  
        }
    }

    private bool CanMove(Vector3 nextPoint)
    {
        RaycastHit hit;
        //Debug.DrawRay(nextPoint, Vector3.down, Color.red, 0.01f);
        //Debug.Log("Ground: " + Physics.Raycast(nextPoint, Vector3.down, out hit, 9f, groundLayer));
        //Debug.Log("Stair: " + Physics.Raycast(nextPoint, Vector3.down, out hit, 9f, stairLayer));

        if(Physics.Raycast(nextPoint, Vector3.down, out hit, 9f, stairLayer))
        {
            int stairColor = hit.collider.gameObject.GetComponent<Stair>().stairColor;
            if (colorIndex != stairColor)
            {
                //Check con gach hay khong
                if(totalBrick > 0)
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

    //private void ChangeAnim(string animName)
    //{
    //    if (currentAnimName != animName)
    //    {
    //        animator.ResetTrigger(animName);
    //        currentAnimName = animName;
    //        animator.SetTrigger(currentAnimName);
    //    }
    //}

    //private void RemoveBrick()
    //{
    //    if (totalBrick > 0)
    //    {
    //        Destroy(brickStack[totalBrick - 1].gameObject);
    //        brickStack.RemoveAt(totalBrick - 1);    
    //        totalBrick--;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Brick") && colorIndex == other.gameObject.GetComponent<Brick>().brickColor)
    //    {
    //        //Debug.Log("Da va cham voi brick");
    //        GameObject brick = other.gameObject;
    //        if (!brickStack.Contains(brick))
    //        {
    //            brickStack.Add(brick);
    //            brick.transform.SetParent(stack.transform);

    //            brick.transform.localRotation = Quaternion.Euler(0, 90, 0);

    //            float stackHeight = brickStack.Count * brick.transform.localScale.y * 1.5f;
    //            Vector3 offset = new Vector3(0, stackHeight, -brick.transform.localScale.z);
    //            brick.transform.localPosition = offset;
    //            totalBrick++;

    //            //StageController.Instance.CreateNewBrick(other.GetComponent<Brick>().brickPosition);
    //            other.gameObject.GetComponent<Brick>().stage.CreateNewBrick(other.GetComponent<Brick>().brickPosition);
    //        } 
    //    }

    //    if (other.CompareTag("Stage"))
    //    {
    //        //debug.log("da va cham voi stage 2");
    //        //StageController.Instance.CharacterStartGame(colorIndex);
    //        other.gameObject.GetComponent<Stage>().stage.CharacterStartGame(colorIndex);
    //    }
    //}
}
