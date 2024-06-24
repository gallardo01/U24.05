using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 moveDirection;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Animator anim;
    private string currentAnimName;

    [SerializeField] Transform brickParent;
    private Stack<Transform> bricks = new();
    private int brickCount = 0;

    private float brickHeight = 0.1f;

    void Start()
    {
        UIManager.Ins.OpenUI<UIGameplay>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = JoystickControl.direct;

        if (moveDirection.magnitude > 0)
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            transform.forward = moveDirection;
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

    private void AddBrick(Transform brickObj)
    {
        bricks.Push(brickObj);
        brickObj.SetParent(brickParent);
        brickObj.localPosition = new Vector3(0, brickHeight * brickCount, 0);

        brickCount++;
    }

    private bool RemoveBrick()
    {
        if (brickCount > 0)
        {
            Destroy(bricks.Pop().gameObject);
            brickCount--;

            return true;
        }
        else
        {
            return false;
        }
    }
}
