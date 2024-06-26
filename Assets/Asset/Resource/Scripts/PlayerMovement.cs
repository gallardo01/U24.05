using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Animator animator;
    [SerializeField] Transform body;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    private string currentAnimName = "idle";
    private Vector3 moveDirection;
    private int colorIndex; public int ColorIndex => colorIndex;

    void Update()
    {
        moveDirection = JoystickControl.direct.normalized;

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

    public void SetPlayerColor(int color)
    {
        colorIndex = color;
        skinnedMeshRenderer.material = ColorController.Instance.GetColor(colorIndex);
    }
}
