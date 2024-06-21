using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 moveDirection;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform playerTF;

    [SerializeField] Animator anim;
    private string currentAnimName;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = JoystickControl.direct;

        if (moveDirection.magnitude > 0)
        {
            playerTF.position += moveDirection * Time.deltaTime * moveSpeed;
            playerTF.forward = moveDirection;
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        }
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
