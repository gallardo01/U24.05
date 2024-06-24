using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    private string currentAnim = "idle";
    public Transform mesh;
    public int colorIndex = 0;
    public SkinnedMeshRenderer body;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetPlayerColor(int color)
    {
        colorIndex = color;
        body.material = ColorController.Ins.GetMaterialColor(colorIndex);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;
        
        if (direction.magnitude > 0f)
        {
            transform.Translate(direction * Time.deltaTime * 5f);
            ChangeAnim("run");
            mesh.forward = JoystickControl.direct;
        }
        else
        {
            ChangeAnim("idle");
        }
    }


    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Brick" && colorIndex == other.GetComponent<Brick>().brickColor)
        {
            //other.gameObject.SetActive(false);
        }
    }
}
