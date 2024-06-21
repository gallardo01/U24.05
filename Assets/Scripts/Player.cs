using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Transform mesh;
    public Animator animator;
    private string currentAnim = "idle";


    void Start()
    {
        
    }

    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction.Normalize();

        if(direction.magnitude > 0f)
        {
            transform.Translate(direction * Time.deltaTime * speed);
            mesh.forward = JoystickControl.direct;
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        } 
    }

    public void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }    
    }    
}
