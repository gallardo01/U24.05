using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public string currentAnimName = "idle";
    public Transform mesh;
    public float speed;
    public int colorIndex;
    public SkinnedMeshRenderer body;

    public GameObject stack;

    private List<GameObject> brickStack = new List<GameObject>(); 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPlayerColor(int color)
    {
        colorIndex = color;
        body.material = ColorController.Instance.GetMaterialColor(colorIndex);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;   
        
        if(direction.magnitude > 0f)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            ChangeAnim("run");
            mesh.forward = JoystickControl.direct;
        }
        else
        {
            ChangeAnim("idle");  
        }
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick") && colorIndex == other.gameObject.GetComponent<Brick>().brickColor)
        {
            Debug.Log("Da va cham voi brick");
            GameObject brick = other.gameObject;
            if (!brickStack.Contains(brick))
            {
                brickStack.Add(brick);
                brick.transform.SetParent(stack.transform);
                other.gameObject.SetActive(false);
            } 
        }
    }
}
