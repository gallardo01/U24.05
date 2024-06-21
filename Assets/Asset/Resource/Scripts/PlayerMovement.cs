using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Animator animator;
    [SerializeField] Transform body;
    private string currentAnimName = "idle";

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(JoystickControl.direct.x, 0, JoystickControl.direct.z).normalized;

        if(moveDirection.magnitude > 0)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
            body.transform.forward = moveDirection;

            ChangeAnim("run");
        }
        else { ChangeAnim("idle"); }
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
}
